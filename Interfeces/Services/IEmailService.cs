using CarsShop.Configuration;
using CarsShop.Db.Models;
using MimeKit;

namespace CarsShop.Interfeces.Services
{
    public interface IEmailService
    {
        Task<string> SendRequest(MailboxAddress From, IEnumerable<MailboxAddress> to, MimeEntity body, string subject, EmailSettingsConfig smtpConfig);
    }
}
