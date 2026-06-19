
using CarsShop.Configuration;
using CarsShop.Db.Models;
using CarsShop.Db;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace CarsShop.Services
{
    public class EmailService
    {
        private readonly EmailSettingsConfig _smtpConfig;
        private readonly AppDbContext _context;

        public EmailService(AppDbContext context, IOptions<EmailSettingsConfig> smtpConfig)
        {
            _context = context;
            _smtpConfig = smtpConfig.Value;
        }

        // MAIN ENTRY
        public async Task SendFromRequest(VehicleRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var user = await _context.Users.FindAsync(request.UserId);
            var car = await _context.Vehicles.FindAsync(request.VehicleId);

            if (user == null)
                throw new Exception($"User not found: {request.UserId}");

            if (car == null)
                throw new Exception($"Vehicle not found: {request.VehicleId}");

            await SendEmail(
                user.FirstName,
                user.LastName,
                user.Phone,
                user.Email,
                car.Model,
                car.Color,
                car.Price,
                request.Message
            );
        }

        // SMTP SENDER
        public async Task SendEmail(
            string firstName,
            string lastName,
            string phone,
            string email,
            string model,
            string color,
            decimal price,
            string message)
        {
            var mimeMessage = new MimeMessage();

            mimeMessage.From.Add(new MailboxAddress("Car Shop", _smtpConfig.SmtpUser));
            mimeMessage.To.Add(new MailboxAddress("Admin", _smtpConfig.SmtpUser));
            mimeMessage.Subject = "New Vehicle Request";

            mimeMessage.Body = new TextPart("plain")
            {
                Text =
                    $"First Name: {firstName}\n" +
                    $"Last Name: {lastName}\n" +
                    $"Phone: {phone}\n" +
                    $"Email: {email}\n" +
                    $"Model: {model}\n" +
                    $"Color: {color}\n" +
                    $"Price: {price}\n" +
                    $"Message: {message}"
            };

            using var client = new SmtpClient();

            await client.ConnectAsync(
                _smtpConfig.SmtpServer,
                _smtpConfig.SmtpPort,
                SecureSocketOptions.StartTls
            );

            await client.AuthenticateAsync(
               //  _smtpConfig.SmtpUser,
               //  _smtpConfig.SmtpPass
               "alberteliav434@gmail.com",
                "rwtq mkdj tbea duer"
            );

            await client.SendAsync(mimeMessage);
            await client.DisconnectAsync(true);
        }
    }
}