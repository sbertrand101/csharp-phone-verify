using Nancy;
using System.Linq;

namespace PhoneVerificator
{
  public class Routes : NancyModule
  {
    public Routes(DatabasebContext database)
    {
      Get["/"] = _ => Response.AsRedirect("/log");

      Get["/log"] = _ =>  View["Log", new { LogEntries = database.LogEntries.ToArray() }];

      Get["/verify"] = _ => View["Verify"];
    }
  }
}
