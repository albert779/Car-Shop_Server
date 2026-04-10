using CarsShop.Db;
using System.Threading.Tasks;

namespace CarsShop.Services
{
    public interface IRequestInfoService
    {
        Task<RequestInfo> AddRequestAsync(RequestInfo request);
    }

    public class RequestInfoService : IRequestInfoService
    {
        private readonly AppDbRequestInfo _context;
        private readonly EmailService _emailService;

        public RequestInfoService(AppDbRequestInfo context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<RequestInfo> AddRequestAsync(RequestInfo request)
        {
            _context.RequestInfos.Add(request);
            await _context.SaveChangesAsync();

            // Optional: send email notification
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