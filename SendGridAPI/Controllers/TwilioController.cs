using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace SendGridAPI.Controllers
{
    [ApiController]
    [Route("v1.0/twilio")]
    public class TwilioController : Controller
    {
        private readonly ILogger<TwilioController> _logger;

        public TwilioController(ILogger<TwilioController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("send")]
        public async Task<IActionResult> SendOTPAsync()
        {
            try
            {
                var accountSid = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Twilio")["SID"];
                var authToken = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Twilio")["AuthKey"];
                TwilioClient.Init(accountSid, authToken);

                var messageOptions = new CreateMessageOptions(
                    new PhoneNumber("YOUR_PHONE_NUMBER"));
                messageOptions.MessagingServiceSid = "YOUR_MESSENGER_SERVICE_ID";
                messageOptions.Body = "Hello Dinuka! This is 2nd Test Msg - Enadoc Team";

                var message = MessageResource.Create(messageOptions);

                return Ok(message.Body);
            }
            catch (Exception ex)
            {
                return (IActionResult)ex;
            }
        }
    }
}
