
using CarsShop.Db;
using CarsShop.Db.Models;
using CarsShop.Dto.RequestsDto.Vehicle.Item;
using CarsShop.Dto.Responses.VehicleShop;
using CarsShop.Interfeces.Db;
using Microsoft.EntityFrameworkCore;

namespace CarsShop.Services
{

    public class VehicleService : IVehicleService
    {
        //private readonly AppDbCar _context;
        private readonly AppDbContext _context;

        //public CarService(AppDbCar context)
        public VehicleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GetVehicleResponse> AddAsync(VehicleItemCreateDto request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var entity = VehicleItemCreateDto.ConvertToDbModel(request);

            await _context.Vehicles.AddAsync(entity);
            await _context.SaveChangesAsync();

            return GetVehicleResponse.ConvertToResponseFromDbModel(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Vehicles.FindAsync(id);
            if (entity == null) return false;

            _context.Vehicles.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<GetVehicleResponse?> GetByIdAsync(int id)
        {
            var entity = await _context.Vehicles.FindAsync(id);

            if (entity == null)
                return null;

            return GetVehicleResponse.ConvertToResponseFromDbModel(entity);
        }


        public async Task<IEnumerable<GetVehicleResponse>> GetCarsAsync(string? search)
        {
            var query = _context.Vehicles.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var lower = search.ToLower();
                query = query.Where(c =>
                    (c.Model != null && c.Model.ToLower().Contains(lower)) ||
                    (c.Color != null && c.Color.ToLower().Contains(lower))
                );
            }

            return await query
                .Select(c => new GetVehicleResponse
                {
                    Id = c.Id,
                    Model = c.Model,
                    Color = c.Color,
                    Price = c.Price,
                    Date = DateOnly.FromDateTime(c.Date),
                    Image = c.Image,
                    Details = c.Details
                })
                .ToListAsync();
        }

        /*
        public async Task<IEnumerable<GetVehicleResponse>> GetListAsync(string? search)
        {
            return await GetCarsAsync(search);
        }
        */

        public async Task<IEnumerable<GetVehicleResponse>> GetListAsync(string? typeName)
        {
            var query = _context.Vehicles
                .Include(v => v.VehicleType)
                .AsQueryable();

            if (!string.IsNullOrEmpty(typeName))
            {
                query = query.Where(v => v.VehicleType.Name == typeName);
            }

            return await query
                .Select(v => new GetVehicleResponse
                {
                    Id = v.Id,
                    Model = v.Model,
                    Color = v.Color,
                    Price = v.Price,
                    Image = v.Image,
                    Date = DateOnly.FromDateTime(v.Date),
                    VehicleType = v.VehicleType.Name
                })
                .ToListAsync();
        }
        public async Task<GetVehicleResponse?> UpdateAsync(int vehicleId, VehicleItemUpdateDto request)
        {
            var entity = await _context.Vehicles.FindAsync(vehicleId);
            if (entity == null) return null;

            entity.Date = request.Date.ToDateTime(TimeOnly.MinValue);
            entity.Model = request.Model;
            entity.Color = request.Color;
            entity.Price = request.Price;
            entity.Details = request.Details;
            entity.Image = request.Image;

            await _context.SaveChangesAsync();
            return GetVehicleResponse.ConvertToResponseFromDbModel(entity);
        }
    }
}

