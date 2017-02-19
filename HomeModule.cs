namespace lily
{
    using System;
    using Nancy;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Twilio;
    using Twilio.Rest.Api.V2010.Account;
    using Twilio.Types;

    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get("/", _ =>
            {
                return "Welcome to Twilio app written with NancyFx and .Net Core";
            });
            Get("/testmessage", _ =>
            {
                var accountSid = "<your account sid>";
                var authToken = "<your auth token>";
                TwilioClient.Init(accountSid, authToken);
                var to = new PhoneNumber("+19998887777");
                var from = new PhoneNumber("+15556667777");
                var body = "Nancy is cool";
                var message = MessageResource.Create(to, accountSid, from, body: body);
                return message.ToString();
            });
            Post("/twilio", parameter =>
            {
                foreach (var key in Request.Form.Keys)
                {
                    Console.Write(key + ": ");
                    Console.WriteLine((string)Request.Form[key]);
                }
                var serializerSetting = new JsonSerializerSettings
                {
                    Error = delegate (object sender, ErrorEventArgs args)
                    {
                        args.ErrorContext.Handled = true;
                    }
                };
                Console.WriteLine(JsonConvert.SerializeObject(Request, Formatting.Indented, serializerSetting));
                return "";
            });
        }
    }
}
