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
        private readonly EmailService _emailService;

        public RequestSubmitterService(AppDbContext appDbContext, EmailService emailService)
        {
            //_carInfoRequestDb = appDbContext;
            _context = appDbContext;
            
            _emailService = emailService;
        }

        /*
        public async Task<VehicleRequest> AddNew(VehicleRequestCreateDto request, int userIdRequested)
        {
            var date = DateTime.Now.ToUniversalTime();
            //RequestStatus status = new RequestStatus() { Id = (int)};
            //VehicleType vehicleType = new VehicleType() { Id = 1 };
            VehicleRequest record = VehicleRequestCreateDto.ConvertToDbModel(request, userIdRequested, date, date, RequestStatusEnum.Pending);
            //await _context.CarInfoRequests.AddAsync(record);
            await _context.VehicleRequest.AddAsync(record);
            await _context.SaveChangesAsync();
            return record;
        }
        */


        /*
        public async Task<VehicleRequest> AddNew(VehicleRequestCreateDto request, int userIdRequested)
        {
            var currentDateTime = DateTime.UtcNow;

            var entity = new VehicleRequest
            {
                VehicleId = request.CarId,
                UserId = userIdRequested,
                CreatedAt = currentDateTime,
                LastUpdate = currentDateTime,
                Message = request.Message,

                RequestStatusId = (int)RequestStatusEnum.Pending
            };

            await _context.VehicleRequest.AddAsync(entity);
            await _context.SaveChangesAsync();

            await _emailService.SendFromRequest(entity);

            return entity;
        }
        */

        public async Task<VehicleRequest> AddNew(VehicleRequestCreateDto request, int userIdRequested)
        {
            Console.WriteLine($"userIdRequested = {userIdRequested}");
            var currentDateTime = DateTime.UtcNow;

            var userExists = await _context.Users
                .AnyAsync(x => x.Id == userIdRequested);

            if (!userExists)
                throw new Exception($"UserId {userIdRequested} does not exist in database");

            var entity = new VehicleRequest
            {
                VehicleId = request.CarId,
                UserId = userIdRequested,
                CreatedAt = currentDateTime,
                LastUpdate = currentDateTime,
                Message = request.Message,
                RequestStatusId = (int)RequestStatusEnum.Pending
            };

            await _context.VehicleRequest.AddAsync(entity);
            await _context.SaveChangesAsync();

            await _emailService.SendFromRequest(entity);

            return entity;
        }
    }
}
    

