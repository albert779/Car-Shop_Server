using CarsShop.Db;
using CarsShop.Dto.Responses;
using CarsShop.Interfeces.Services;
using Microsoft.EntityFrameworkCore;

namespace CarsShop.Services
{
    public class MyRequestsService : IMyRequestsService
    {
        private readonly AppDbContext _context;

        public MyRequestsService(AppDbContext context)
        {
            _context = context;
        }


        /*
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
        */
        public async Task<DashboardResponse> GetDashboardAsync(int userId)
        {
            Console.WriteLine($"Dashboard UserId: {userId}");

            var total = await _context.VehicleRequests
                .CountAsync(x => x.UserId == userId);

            Console.WriteLine($"Total Requests: {total}");

            return new DashboardResponse
            {
                TotalRequests = total,

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
    }
}