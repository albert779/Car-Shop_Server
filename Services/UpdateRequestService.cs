using CarsShop.Db;
using CarsShop.Db.Models;
using CarsShop.Dto.Requests;
using CarsShop.Interfeces.Services;
using Microsoft.EntityFrameworkCore;

namespace CarsShop.Services
{
    public class UpdateRequestService : IUpdateRequestService
    {
        private readonly AppDbContext _context;

        public UpdateRequestService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<VehicleRequest?> UpdateRequestAsync(
            int id,
            UpdateRequestDto dto)
        {
            var existingRequest = await _context.VehicleRequests
                .FirstOrDefaultAsync(x => x.Id == id);

            if (existingRequest == null)
            {
                return null;
            }

            existingRequest.RequestStatusId = dto.StatusId;
            existingRequest.LastUpdate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return existingRequest;
        }
    }
}