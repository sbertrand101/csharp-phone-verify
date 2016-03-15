# Phone Verication App

This example app shows how you can use the Catapult API verify a phone number by sending to it a SMS or phone call with confirm code. 

Uses the:
* [Catapult .Net SDK](https://github.com/bandwidthcom/csharp-bandwidth)
* [Making calls](http://ap.bandwidth.com/docs/rest-api/calls/#resourcePOSTv1usersuserIdcalls/?utm_medium=social&utm_source=github&utm_campaign=dtolb&utm_content=_)
* [Sending sms](http://ap.bandwidth.com/docs/rest-api/messages/#resourcePOSTv1usersuserIdmessages/?utm_medium=social&utm_source=github&utm_campaign=dtolb&utm_content=_)
* [Bandwidth XML](http://ap.bandwidth.com/docs/xml/?utm_medium=social&utm_source=github&utm_campaign=dtolb&utm_content=_)


## Prerequisites
- Configured Machine with Ngrok/Port Forwarding -OR- Azure Account
  - [Ngrok](https://ngrok.com/)
  - [Azure](https://account.windowsazure.com/Home/Index)
- [Catapult Account](https://catapult.inetwork.com/pages/signup.jsf/?utm_medium=social&utm_source=github&utm_campaign=dtolb&utm_content=_)
- [Visual Studio 2015](https://www.visualstudio.com/en-us/downloads/download-visual-studio-vs.aspx)
- [Git](https://git-scm.com/)
- Common Azure Tools for Visual Studio (they are preinstalled with Visual Studio)

## Build and Deploy

### Azure One Click

#### Settings Required To Run
* ```Catapult User Id```
* ```Catapult Api Token```
* ```Catapult Api Secret```

[![Deploy to Azure](http://azuredeploy.net/deploybutton.png)](https://azuredeploy.net/)

## Demo
![Screen Shot](/readme_images/screenshot.png?raw=true)
Open the app in web browser. You will list of verified phone numbers. To verify a phone number go to `/verify`.


### Locally

Clone the web application.

```console
git clone https://github.com/BandwidthExamples/csharp-phone-verificator.git
```

Before run them fill config files (Web.config) with right values:

`catapultUserId`, `catapultApiToken`, `catapultApiSecret` - auth data for Catapult API,

Open solution file in Visual Studio and build it.

You can run compiled C# code with IIS Express on local machine if you have ability to handle external requests or use any external hosting (like Azure).
