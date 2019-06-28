using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;
using Newtonsoft.Json;

namespace Demos
{
    public static class SendMessage
    {
        [FunctionName("SendMessage")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            //[SendGrid(ApiKey = "SendGridAPIKey")] out SendGridMessage message,
            IBinder binder,
            ILogger log)
        {
            IActionResult result;

            try 
            {
                string key = req.Query["key"];
                string to = req.Query["to"];

                var sender = await binder.BindAsync<ICollector<SendGridMessage>>(new SendGridAttribute(){
                    ApiKey = key
                });

                string apiKey = Environment.GetEnvironmentVariable(key);

                var message = new SendGridMessage();
                message.From = new EmailAddress(Environment.GetEnvironmentVariable("EmailSender"));
                message.Subject = "Dynamic SendGrid API Key";
                message.HtmlContent = $"Using key: {apiKey.Substring(0, 5)}";
                message.AddTo(to);

                sender.Add(message);

                result = (ActionResult)new OkObjectResult("Message sent");
            }
            catch(Exception e)
            {
                log.LogError("Demos.SendMessage", e);
                result = new BadRequestObjectResult("Failed to send message");
            }

            return result;
        }
    }
}
