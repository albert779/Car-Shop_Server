using CarsShop.Configuration;
using CarsShop.Db;
using CarsShop.Db.Models;
using CarsShop.Dto.Responses;
using CarsShop.Interfeces.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Drawing;
using System.Numerics;
using CarsShop.Dto.RequestsDto;
using CarsShop.Dto.Responses.VehicleRequests;
using CarsShop.Dto;
using CarsShop.Dto.Responses;

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
        //---------------------
        public async Task<PagedResult<VehicleRequestResponse>> GetRequestsAsync(
        int userId,
        RequestFilterDto filter)
        {
            var query = _context.VehicleRequests
                .Include(x => x.Vehicle)
                    .ThenInclude(v => v.VehicleType)
                .Include(x => x.Status)
                .Where(x => x.UserId == userId);


            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                query = query.Where(x =>
                    x.Vehicle.Model.Contains(filter.Search) ||
                    x.Message.Contains(filter.Search));
            }


            if (filter.StatusId.HasValue)
            {
                query = query.Where(x =>
                    x.RequestStatusId == filter.StatusId.Value);
            }


            var total = await query.CountAsync();


            var data = await query
                .OrderByDescending(x => x.CreatedAt)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(x => new VehicleRequestResponse
                {
                    Id = x.Id,
                    VehicleId = x.VehicleId,
                    Model = x.Vehicle.Model,
                    Message = x.Message,
                    Status = x.Status.Name,
                    CreatedAt = x.CreatedAt
                })
                .ToListAsync();


            return new PagedResult<VehicleRequestResponse>
            {
                Items = data,
                TotalCount = total,
                Page = filter.Page,
                PageSize = filter.PageSize
            };
        }
    





        //----------------------------
        public async Task AddNew(VehicleRequestCreateDto request, int userIdRequested)
        {
            var from = new MailboxAddress("Car Shop", _emailSettingsConfig.SmtpUser);

            var user = await _context.Users.SingleAsync(user => user.Id == userIdRequested);
            var toUserEntety = new MailboxAddress(user.FirstName, user.Email);
            var to  = new List<MailboxAddress>(1) { toUserEntety };
            var currentDateTime = DateTime.UtcNow;
            
            var vehicle = await _context.Vehicles.SingleAsync(vehicle => vehicle.Id == request.CarId);
            var body = BuildBody(user, vehicle, request.Message);

            
            // ✅ 1. CREATE DB ENTITY
            var entity = new VehicleRequest
            {
                VehicleId = request.CarId,
                UserId = userIdRequested,
                Message = request.Message,
                CreatedAt = DateTime.UtcNow,

                // ⭐ STATUS ADDED HERE
                RequestStatusId = (int)RequestStatusEnum.Pending
            };

            // ✅ 2. SAVE TO DATABASE
            _context.VehicleRequests.Add(entity);
            await _context.SaveChangesAsync();

            await _emailService.SendRequest(from, to, body, _subjectEmail, _emailSettingsConfig);

            // misisng save in DB on the status
            
        }

        public async Task<DashboardResponse> GetDashboardAsync(int userId)
        {
            return new DashboardResponse
            {
                TotalRequests = await _context.VehicleRequests
                    .CountAsync(x => x.UserId == userId),

                PendingRequests = await _context.VehicleRequests
                    .CountAsync(x => x.UserId == userId &&
                                     x.RequestStatusId == (int)RequestStatusEnum.Pending),

                ApprovedRequests = await _context.VehicleRequests
                    .CountAsync(x => x.UserId == userId &&
                                     x.RequestStatusId == (int)RequestStatusEnum.Approved),

                RejectedRequests = await _context.VehicleRequests
                    .CountAsync(x => x.UserId == userId &&
                                     x.RequestStatusId == (int)RequestStatusEnum.Rejected)
            };
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
