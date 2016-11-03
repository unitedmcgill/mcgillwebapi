using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace McGillWebAPI.Services
{
    public class MailService : IMailService
    {
        private IConfigurationRoot _config; 

        public MailService(IConfigurationRoot config)
        {
            _config = config;
        }

        public async Task SendMail(string to, string name, string from, string subject, string body)
        {
            //throw new NotImplementedException();
            //Debug.WriteLine($"Sending Mail: To: {to} From: {from} Subject: {subject}");
            var emailMessage = new MimeMessage();
            
            emailMessage.From.Add(new MailboxAddress(name, from));
            emailMessage.To.Add(new MailboxAddress("",to));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text = body};

            using ( var client = new SmtpClient())
            {
                client.LocalDomain = "unitedmcgill.com";
                await client.ConnectAsync(_config["MailSettings:Server"], 
                    Convert.ToInt32(_config["MailSettings:Port"]), 
                    SecureSocketOptions.None).ConfigureAwait(false);
                await client.SendAsync(emailMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
        }
    }
}
