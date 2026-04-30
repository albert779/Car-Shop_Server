using CarsShop.Db;
using CarsShop.Db.Models;
using CarsShop.Interfeces.Services;
using Microsoft.EntityFrameworkCore;

namespace CarsShop.Services
{
    public class RequestSubmitterService : IRequestSubmitterService
    {
        //private AppDbContext _carInfoRequestDb;
        private readonly AppDbContext _context;

        public RequestSubmitterService(AppDbContext appDbContext)
        {
            //_carInfoRequestDb = appDbContext;
            _context = appDbContext;
        }

        public async Task<VehicleRequest> AddNew(VehicleRequestCreateDto request, int userIdRequested)
        {
            var date = DateTime.Now.ToUniversalTime();
            RequestStatus status = new RequestStatus() { Id = 1 };
            VehicleType vehicleType = new VehicleType() { Id = 1 };
            VehicleRequest record = VehicleRequestCreateDto.ConvertToDbModel(request, status, userIdRequested, date, date, status, vehicleType);
            await _context.CarInfoRequests.AddAsync(record);
            await _context.SaveChangesAsync();
            return record;
        }
    }
}
