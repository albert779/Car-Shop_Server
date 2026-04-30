
using CarsShop.Db;
using CarsShop.Db.Models;
using System.Threading.Tasks;

namespace CarsShop.Services
{
    public interface ITruckRequestService
    {
        Task<VehicleRequest> AddRequestAsync(VehicleRequest request);
    }

    public class TruckRequestService : ITruckRequestService
    {
        private readonly AppDbContext _context;
        private readonly EmailService _emailService;
       
        public TruckRequestService(AppDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }
       
        public async Task<VehicleRequest> AddRequestAsync(VehicleRequest request)
        {
            _context.TruckRequestInfos.Add(request);
            await _context.SaveChangesAsync();

            // Send email notification
            
            await _emailService.SendEmail(
                request.User.FirstName,
                request.User.LastName,
                request.User.Phone,
                request.User.Email,
                request.Vehicle.Model,
                request.Vehicle.Color,
                request.Vehicle.Price,
                request.Message
            );

            return request;
            
        }
    }
}
