using MongoDB.Driver;

namespace ViewUsers
{
  public class UsersCollection
  {
    public object users = new MongoClient("mongodb://localhost:3001/")
      .GetDatabase("meteor").GetCollection<Users>("users").Find(_ => true).ToList();
  }
}
