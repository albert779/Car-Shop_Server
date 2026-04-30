
/*
using CarsShop.Db;
using CarsShop.Interfeces.Db;
using CarsShop.RequestsDto.TrucksShop;
using CarsShop.Responses.TrucksShop;
using Microsoft.EntityFrameworkCore;
using CarsShop.Db.Models;

namespace CarsShop.Services
{
    public class TruckService : ITruckService
    {
        //private readonly AppDbTruck _db;
        //private readonly AppDbTruck _context;
        private readonly AppDbContext _context;

        //public TruckService(AppDbTruck db)
        public TruckService(AppDbContext context)
        {
            _context = context;
        }

        // CREATE
        public async Task<Vehicle> CreateAsync(TrucksCreateDto dto)
        {
            var entity = new Vehicle
            {
                Model = dto.Model,
                Color = dto.Color,
                Price = dto.Price,
                Details = dto.Details,
                Image = dto.Image,
                Date = dto.Date
            };

            await _context.Vehicles.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // GET BY ID
        public async Task<Vehicle> GetByIdAsync(int id)
        {
            return await _context.Vehicles.FindAsync(id);
        }

        // UPDATE
        public async Task<bool> UpdateAsync(int id, TrucksUpdateDto dto)
        {
            var entity = await _context.Vehicles.FindAsync(id);
            if (entity == null) return false;

            entity.Model = dto.Model;
            entity.Color = dto.Color;
            entity.Price = dto.Price;
            entity.Details = dto.Details;
            entity.Image = dto.Image;
            entity.Date = dto.Date;

            _context.Vehicles.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Vehicles.FindAsync(id);
            if (entity == null) return false;

            _context.Vehicles.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<GetCarstResponse>> GetTrucksAsync(string? search)
        {
            var query = _context.Vehicles.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim().ToLower();

                query = query.Where(t =>
                    (t.Model != null && t.Model.ToLower().Contains(s)) ||
                    (t.Color != null && t.Color.ToLower().Contains(s))
                );
            }

            return await query
                .Select(t => new GetCarstResponse
                {
                    Id = t.Id,
                    Model = t.Model,
                    Color = t.Color,
                    Price = t.Price,
                    Date = t.Date,
                    Image = t.Image,
                    Details = t.Details
                })
                .ToListAsync();
        }

        // GET ALL
        public async Task<IEnumerable<GetCarstResponse>> GetListAsync()
        {
            var trucks = await _context.Vehicles.ToListAsync();
            return trucks.Select(GetCarstResponse.ConvertToResponseFromDbModel);
        }

        public Task<IEnumerable<GetCarstResponse>> GetListAsync(string? search)
        {
            throw new NotImplementedException();
        }
    }
}

*/