using Bandwidth.Net.Model;
using Bandwidth.Net.Xml.Verbs;
using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace PhoneVerificator
{
  public class Routes : NancyModule
  {
    public Routes(DatabasebContext database)
    {
      Get["/"] = _ => Response.AsRedirect("/log");

      Get["/log"] = _ => View["Log", new { LogEntries = database.LogEntries.Where(e => e.VerifiedTime != null).OrderByDescending(e => e.VerifiedTime).ToArray() }];

      Get["/verify"] = _ => View["Verify"];

      Get["/result"] = _ => View["Result"];

      Get["/call-callback"] = _ =>
      {
        var code = (string)Request.Query.Tag;
        if (string.IsNullOrEmpty(code) && Request.Query.EventType != "answer")
        {
          return "";
        }
        // speak code to user and hang up
        return new Bandwidth.Net.Xml.Response(new SpeakSentence
        {
          Sentence = $"Your confirm code is {code}",
          Locale = "en_US",
          Gender = "female",
          Voice = "julie"
        }, new Hangup()).ToXml();
      };

      Post["/verify", true] = async (_, t) =>
      {
        var form = this.Bind<VerifyForm>();
        var code = new Random().Next(1000, 9999).ToString();
        var servicePhoneNumber = await GetServicePhoneNumber();
        if (form.Action.ToLowerInvariant() == "call")
        {
          await Call.Create(new Dictionary<string, object>
          {
            { "from", servicePhoneNumber },
            { "to", form.PhoneNumber },
            { "callbackHttpMethod", "GET"},
            { "callbackUrl", $"{Request.Url.BasePath}/call-callback" },
            { "tag", code }
          });
        }
        else
        {
          await Message.Create(new Dictionary<string, object>
          {
            { "from", servicePhoneNumber },
            { "to", form.PhoneNumber },
            { "text", $"Your confirm code is {code}" }
          });
        }
        var entry = database.LogEntries.Add(new PhoneVerificationLogEntry
        {
          PhoneNumber = form.PhoneNumber,
          Code = code
        });
        await database.SaveChangesAsync();
        return View["Code", entry];
      };

      Post["/code", true] = async (_, t) =>
      {
        var form = this.Bind<CodeForm>();
        var entry = database.LogEntries.FirstOrDefault(e => e.Code == form.Code && e.PhoneNumber == form.PhoneNumber);
        if (entry == null)
        {
          return Response.AsRedirect("/verify");
        }
        entry.VerifiedTime = new DateTime();
        await database.SaveChangesAsync();
        return Response.AsRedirect("/result");
      };
    }

    private async Task<string> GetServicePhoneNumber()
    {
      var phoneNumber = ConfigurationManager.AppSettings.Get("servicePhoneNumber");
      if (phoneNumber == null)
      {
        phoneNumber = (await AvailableNumber.SearchLocal(new Dictionary<string, object>
        {
            {"state", "NC"},
            {"quantity", 1}
        })).First().Number;
        await PhoneNumber.Create(new Dictionary<string, object>
        {
            {"number", phoneNumber},
        });
        var config = WebConfigurationManager.OpenWebConfiguration("~");
        config.AppSettings.Settings.Add("servicePhoneNumber", phoneNumber);
        config.Save(ConfigurationSaveMode.Minimal, false);
        ConfigurationManager.RefreshSection("appSettings");
      }
      return phoneNumber;
    }
  }

  public class VerifyForm
  {
    public string Action { get; set; }
    public string PhoneNumber { get; set; }
  }

  public class CodeForm
  {
    public string Code { get; set; }
    public string PhoneNumber { get; set; }
  }

  public class CallCallbackForm
  {
    public string Tag { get; set; }
  }
}
