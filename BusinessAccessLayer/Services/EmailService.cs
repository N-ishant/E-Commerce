using BusinessAccessLayer.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BusinessAccessLayer.Services;
using System.Runtime.InteropServices;

namespace BusinessAccessLayer.Services
{
    public class EmailService : IEmailService

    {

        //private const string templatePath = @"../emailTemplate/{0}.html";
 
         string templatePath = System.IO.Path.Combine("../emailTemplate/{0}.html");


        private readonly IConfiguration _smtpconfig;

        public EmailService(IConfiguration smtpconfig)

        {
            _smtpconfig = smtpconfig;
        }


        public async Task SendEmailAsync(string email, string subject)

        {

            string fromemail = _smtpconfig["SMTPConfig:SenderAddress"];

            string password = _smtpconfig["SMTPConfig:Password"];

            MailMessage message = new MailMessage();

            message.From = new MailAddress(fromemail);

            message.Subject = subject;

            message.To.Add(new MailAddress(email));

           message.Body = GetEmailBody("EmailTemplate");
            //message.Body =  File.ReadAllText(string.Format(templatePath, "EmailTemplate"));

            message.IsBodyHtml = Convert.ToBoolean(_smtpconfig["SMTPConfig:IsBodyHtml"]);

            var smtpClient = new SmtpClient(_smtpconfig["SMTPConfig:Server"])

            {

                Port = Convert.ToInt32(_smtpconfig["SMTPConfig:Port"]),

                Credentials = new NetworkCredential(fromemail, password),

                EnableSsl = Convert.ToBoolean(_smtpconfig["SMTPConfig:EnableSSL"]),

                DeliveryMethod = SmtpDeliveryMethod.Network,

                Timeout = 10000,

            };

            smtpClient.Send(message);

        }
        private string GetEmailBody(string templateName)
        {

            string filePath = string.Format(templatePath, templateName);

            if (File.Exists(filePath))
            {
                var body = File.ReadAllText(filePath);
                return body;
            }
            else
            {
                // Handle the case where the file does not exist
                // For example, throw an exception or return a default template
                return "Order Placed SuccessFully";
            }
        }
    }
}
