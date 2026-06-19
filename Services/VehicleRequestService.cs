using CarsShop.Configuration;
using CarsShop.Db;
using CarsShop.Db.Models;
using CarsShop.Interfeces.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Drawing;
using System.Numerics;

namespace CarsShop.Services
{
    public class VehicleRequestService : IVehicleRequestService
    {
        private readonly IEmailService _emailService;
        private readonly AppDbContext _context;
        private readonly EmailSettingsConfig _emailSettingsConfig;
        private const string _subjectEmail = "New Vehicle Request";


        public VehicleRequestService(IEmailService emailService, AppDbContext context, IOptions<EmailSettingsConfig> smtpConfig)
        {
            _emailService = emailService;
            _context = context;
            _emailSettingsConfig = smtpConfig.Value;
        }


        public async Task AddNew(VehicleRequestCreateDto request, int userIdRequested)
        {
            var from = new MailboxAddress("Car Shop", _emailSettingsConfig.SmtpUser);

            var user = await _context.Users.SingleAsync(user => user.Id == userIdRequested);
            var toUserEntety = new MailboxAddress(user.FirstName, user.Email);
            var to  = new List<MailboxAddress>(1) { toUserEntety };
            var currentDateTime = DateTime.UtcNow;
            
            var vehicle = await _context.Vehicles.SingleAsync(vehicle => vehicle.Id == request.CarId);
            var body = BuildBody(user, vehicle, request.Message);

            await _emailService.SendRequest(from, to, body, _subjectEmail, _emailSettingsConfig);

            // misisng save in DB on the status
            
        }


        private MimeEntity BuildBody(User user, Vehicle vehicle, string message)
        {
            var body = new TextPart("plain")
            {
                Text =
                     $"First Name: {user.FirstName}\n" +
                     $"Last Name: {user.LastName}\n" +
                     $"Phone: {user.Phone}\n" +
                     $"Email: {user.Email}\n" +
                     $"Model: {vehicle.Model}\n" +
                     $"Color: {vehicle.Color}\n" +
                     $"Price: {vehicle.Price}\n" +
                     $"Message: {message}"
            };

            return body;
        }
    }
}
