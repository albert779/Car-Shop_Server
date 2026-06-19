
using CarsShop.Configuration;
using CarsShop.Db;
using CarsShop.Db.Models;
using CarsShop.Interfeces.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Drawing;
using System.Numerics;

namespace CarsShop.Services
{
    public class EmailService : IEmailService
    {
        public EmailService()
        {
        }

        public async Task<string> SendRequest(MailboxAddress From, IEnumerable<MailboxAddress> to, MimeEntity body, string subject, EmailSettingsConfig smtpConfig)
        {

            var payload = BuildPayload(From, to, subject, body);

            using var client = new SmtpClient();
            await client.ConnectAsync(smtpConfig.SmtpServer, smtpConfig.SmtpPort, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(smtpConfig.SmtpUser, smtpConfig.SmtpPass);
            var response = await client.SendAsync(payload);
            return response;
        }


        private MimeMessage BuildPayload(MailboxAddress From, IEnumerable<MailboxAddress> to, string subject, MimeEntity body)
        {
            var payload = new MimeMessage();

            payload.From.Add(From);
            payload.To.AddRange(to);
            payload.Subject = subject;
            payload.Body = body;
            return payload;
        }
    }
}