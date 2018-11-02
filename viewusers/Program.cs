using Mono.Unix;
using Mono.Unix.Native;
using Nancy.Hosting.Self;
using Nancy;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;

namespace ViewUsers
{
  // main loop 
  class Program
  {
    static void Main(string[] args)
    {
       // register class map 
       BsonClassMap.RegisterClassMap<Users>(
         cm => 
         {
          cm.AutoMap();
         }
       );

      // connect to db 
      var client = new MongoClient("mongodb://localhost:3001/");
      var db = client.GetDatabase("meteor");
      var collection = db.GetCollection<Users>("users");
      var users = collection.Find(_ => true).ToList().ToJson();
      Console.WriteLine(collection.Find(_ => true).ToList().ToJson());

      // serve w/ nancy
      var uri = "http://localhost:3002";
      Console.WriteLine("Starting Nancy on " + uri);
      Console.WriteLine("User information is at http://localhost:3002/users");

      // init nancyhost 
      var host = new NancyHost(new Uri(uri));
      host.Start(); // start hosting

      // check for mono 
      if (Type.GetType("Mono.Runtime") != null)
      {
        UnixSignal.WaitAny(new[] {
          new UnixSignal(Signum.SIGINT),
          new UnixSignal(Signum.SIGTERM),
          new UnixSignal(Signum.SIGQUIT),
          new UnixSignal(Signum.SIGHUP),
        });
      }
      else 
      {
        Console.ReadLine();
      }

     

    Console.WriteLine("Stopping Nancy..");
      host.Stop();  // stop hosting
    }
  }

} // end namespace 