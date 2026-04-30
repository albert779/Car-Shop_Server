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

        // ✅ MAIN ENTRY
        public async Task SendFromRequest(VehicleRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            // ✔ LOAD USER + CAR FROM DB
            //var user = await _context.Users.FindAsync(request.UserId);
            //var car = await _context.Vehicles.FindAsync(request.CarId);


            var user = await _context.Users.FindAsync(1);
            var car = await _context.Vehicles.FindAsync(1);

            string firstName = user?.FirstName ?? "";
            string lastName = user?.LastName ?? "";
            string phone = user?.Phone ?? "";
            string email = user?.Email ?? "";

            string model = car?.Model ?? "";
            string color = car?.Color ?? "";
            decimal price = car?.Price ?? 0;

            await SendEmail(
                firstName,
                lastName,
                phone,
                email,
                model,
                color,
                price,
                request.Message
            );
        }

        // ✅ SMTP SENDER
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

            var senderEmail = _smtpConfig.SmtpUser;

            mimeMessage.From.Add(new MailboxAddress("Car Shop", senderEmail));
            mimeMessage.To.Add(new MailboxAddress("Admin", senderEmail));
            mimeMessage.Subject = "New Car Info Request";

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
                    $"Details: {message}"
            };

            using var client = new SmtpClient();

            await client.ConnectAsync(
                _smtpConfig.SmtpServer,
                _smtpConfig.SmtpPort,
                SecureSocketOptions.StartTls
            );

            await client.AuthenticateAsync(
                _smtpConfig.SmtpUser,
                _smtpConfig.SmtpPass
            );

            await client.SendAsync(mimeMessage);
            await client.DisconnectAsync(true);
        }
    }
}