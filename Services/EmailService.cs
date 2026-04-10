using CarsShop.Configuration;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;

public class EmailService
{
    private readonly EmailSettingsConfig _smptConfig;

    public EmailService(IConfiguration config, IOptions<EmailSettingsConfig> SmptConfig)
    {
        _smptConfig = SmptConfig.Value;
    }

    public async Task SendEmail(string firstName, string lastName, string phone, string email, string details)
    {
        var message = new MimeMessage();

        var user = _smptConfig.SmtpUser;


        message.From.Add(new MailboxAddress("Car Shop", user));
        message.To.Add(new MailboxAddress("Admin", user));
        message.Subject = "New Car Info Request";

        message.Body = new TextPart("plain")
        {
            Text =
                $"First Name: {firstName}\n" +
                $"Last Name: {lastName}\n" +
                $"Phone: {phone}\n" +
                $"Email: {email}\n" +
                $"Details: {details}"
        };

        using var client = new SmtpClient();

        //await client.ConnectAsync(
        //    _config["EmailSettings:SmtpServer"],
        //    int.Parse(_config["EmailSettings:SmtpPort"]),
        //    SecureSocketOptions.StartTls
        //);

        //await client.AuthenticateAsync(
        //    user,
        //    _config["EmailSettings:SmtpPass"]
        //);

        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}