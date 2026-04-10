using CarsShop.Db;
using CarsShop.Db.Models;
using System.Threading.Tasks;

namespace CarsShop.Services
{
    public interface ITruckRequestService
    {
        Task<TruckRequestInfo> AddRequestAsync(TruckRequestInfo request);
    }

    public class TruckRequestService : ITruckRequestService
    {
        private readonly AppDbRequestInfo _context;
        private readonly EmailService _emailService;

        public TruckRequestService(AppDbRequestInfo context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<TruckRequestInfo> AddRequestAsync(TruckRequestInfo request)
        {
            _context.TruckRequestInfos.Add(request);
            await _context.SaveChangesAsync();

            // Send email notification
            await _emailService.SendEmail(
                request.FirstName,
                request.LastName,
                request.Phone,
                request.Email,
                request.Details
            );

            return request;
        }
    }
}