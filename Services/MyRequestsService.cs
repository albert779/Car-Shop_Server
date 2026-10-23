using CarsShop.Db;
using CarsShop.Dto.RequestsDto;
using CarsShop.Dto.Responses;
using CarsShop.Interfeces.Services;
using Microsoft.EntityFrameworkCore;
using TableOfRequestsDto =
    CarsShop.Dto.RequestsDto.TableOfRequests.TableOfRequests;
namespace CarsShop.Services
{
    public class MyRequestsService : IMyRequestsService
    {
        private readonly AppDbContext _context;

        public MyRequestsService(AppDbContext context)
        {
            _context = context;
        }


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

        // Add this method inside your existing MyRequestsService class.

        public async Task<MyRequestsPageDto> GetMyRequestsAsync(
            int userId,
            int page,
            int pageSize)
        {
            page = Math.Max(1, page);
            pageSize = Math.Clamp(pageSize, 1, 50);

            var query = _context.VehicleRequests
                .AsNoTracking()
                .Where(r => r.UserId == userId);

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(r => r.CreatedAt)
                .ThenByDescending(r => r.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(r => new TableOfRequestsDto
                {
                    Id = r.Id,
                    UserId = r.UserId,
                    Vehicle = r.Vehicle.Model,
                    Image = r.Vehicle.Image,
                    Message = r.Message,
                    Status = r.Status.Name,
                    RequestedOn = r.CreatedAt,
                    LastUpdate = r.LastUpdate
                })
                .ToListAsync();

            return new MyRequestsPageDto
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(
                    totalCount / (double)pageSize)
            };
        }
    }
}