using System.Configuration;
using Bandwidth.Net;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace PhoneVerificator
{
  using Nancy;

  public class Bootstrapper : DefaultNancyBootstrapper
  {
    protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
    {
      Client.GlobalOptions = new ClientOptions
      {
        UserId = ConfigurationManager.AppSettings.Get("userId"),
        ApiToken = ConfigurationManager.AppSettings.Get("apiToken"),
        ApiSecret = ConfigurationManager.AppSettings.Get("apiSecret"),
      };

      base.ApplicationStartup(container, pipelines);
      container.Register<DatabasebContext>();
    }

  }
}
