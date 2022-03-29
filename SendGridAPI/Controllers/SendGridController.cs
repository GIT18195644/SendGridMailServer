using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using SendGridAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace SendGridAPI.Controllers
{
    [ApiController]
    [Route("v1.0/sendGrid")]
    public class SendGridController : Controller
    {
        private readonly ILogger<SendGridController> _logger;

        public SendGridController(ILogger<SendGridController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("send/single")]
        public async Task<IActionResult> SendSingleEmailAsync([FromBody] EmailModel emailModel)
        {
            try
            {
                var apiKey = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SendGrid")["APIKey"];
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("YOUR_DOMAIN_SERVICE", "Enadoc Communication");
                var subject = emailModel.EmailSubject.ToString();
                var to = new EmailAddress(emailModel.EmailTo.Trim().ToLower().ToString(), emailModel.EmailTo.Split("@")[0]);
                var plainTextContent = emailModel.EmailPlainText.ToString();
                var htmlContent = emailModel.EmailHtmlText;
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);

                return Ok(response.Body);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Exception: SendSingleEmailAsync -- " + ex.Message);
                return (IActionResult)ex;
            }
        }

        [HttpPost]
        [Route("send/single/template")]
        public async Task<IActionResult> SendSingleTemplateEmailAsync([FromBody] EmailTemplateModel emailTemplateModel)
        {
            try
            {
                var apiKey = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SendGrid")["APIKey"];
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("YOUR_DOMAIN_SERVICE", "Enadoc Communication");
                var to = new EmailAddress(emailTemplateModel.EmailTo.Trim().ToLower().ToString(), emailTemplateModel.EmailTo.Split("@")[0]);
                var templateid = emailTemplateModel.templateid;
                var dynamicTemplateData = new Dictionary<string, string>
                {
                    {"name", "<strong>Shanaka</strong>"},
                };
                var msg = MailHelper.CreateSingleTemplateEmail(from, to, templateid, dynamicTemplateData);
                var response = await client.SendEmailAsync(msg);

                return Ok(response.Body);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Exception: SendSingleTemplateEmailAsync -- " + ex.Message);
                return (IActionResult)ex;
            }
        }

        [HttpPost]
        [Route("send/multiple")]
        public async Task<IActionResult> SendMultipleEmailAsync([FromBody] MultiEmailModel multiEmailModel)
        {
            try
            {
                var apiKey = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SendGrid")["APIKey"];
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("YOUR_DOMAIN_SERVICE", "Enadoc Communication");
                var subject = multiEmailModel.EmailSubject.ToString();
                var to = multiEmailModel.EmailTo.Distinct(StringComparer.CurrentCultureIgnoreCase).Select(c => new EmailAddress(c)).ToList();
                var plainTextContent = multiEmailModel.EmailPlainText.ToString();
                var htmlContent = multiEmailModel.EmailHtmlText;
                var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);

                return Ok(response.Body);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Exception: SendMultipleEmailAsync -- " + ex.Message);
                return (IActionResult)ex;
            }
        }

        [HttpPost]
        [Route("send/multiple/template")]
        public async Task<IActionResult> SendMultipleTemplateEmailAsync([FromBody] MultiEmailTemplateModel multiEmailTemplateModel)
        {
            try
            {
                var apiKey = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SendGrid")["APIKey"];
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("YOUR_DOMAIN_SERVICE", "Enadoc Communication");
                var to = multiEmailTemplateModel.EmailTo.Distinct(StringComparer.CurrentCultureIgnoreCase).Select(c => new EmailAddress(c)).ToList();
                var templateid = multiEmailTemplateModel.templateid;
                var dynamicTemplateData = new Dictionary<string, string>
                {
                    {"name", "<strong>Dinuka</strong>"},
                };
                var msg = MailHelper.CreateSingleTemplateEmailToMultipleRecipients(from, to, templateid, dynamicTemplateData);
                var response = await client.SendEmailAsync(msg);

                return Ok(response.Body);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Exception: SendMultipleTemplateEmailAsync -- " + ex.Message);
                return (IActionResult)ex;
            }
        }
    }
}
