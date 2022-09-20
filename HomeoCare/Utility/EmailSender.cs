using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HomeoCare.Utility
{
    public class MailSettings
    {
        public string host { get; set; }
        public int port { get; set; }
        public bool enableSSL { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
    }
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        public MailSettings _mailSettings { get; set; }

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Use our configuration to send the email by using SmtpClient
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                _mailSettings = _configuration.GetSection("EmailSender").Get<MailSettings>();

                var client = new SmtpClient(_mailSettings.host, _mailSettings.port)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(_mailSettings.userName, _mailSettings.password),
                    EnableSsl = _mailSettings.enableSSL

                };
                return client.SendMailAsync(
                    new MailMessage(_mailSettings.userName, email, subject, htmlMessage) { IsBodyHtml = true }
                );
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
