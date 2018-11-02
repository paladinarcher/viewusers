using Nancy;
using System;


namespace ViewUsers
{
  public class ShowUsers: NancyModule
  {
    public ShowUsers()
    {
      Get["/users"] = parameters => "Hello";
    }
  }
}
