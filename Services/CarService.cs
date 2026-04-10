
/*
using CarsShop.Db;
using CarsShop.Db.Models;
using CarsShop.Interfeces.Db;
using CarsShop.RequestsDto.CarsShop;
using CarsShop.Responses.CarsShop;
//using IDGCoreWebAPI.Controllers.CarsShop.Controllers;
using Microsoft.EntityFrameworkCore;
using System;

namespace CarsShop.Services
{

    public class CarService : ICarService
    {
        // private readonly ILogger<WeatherForecastController> _logger;
        private readonly AppDbCar _db;
        //private readonly AppDb _context;
        private readonly AppDbCar _context;


        public CarService(AppDbCar db, AppDbCar context)
        {
            this._db = db;
            _context = context;
        }

        public IEnumerable<GetCarstResponse> GetList()
        {

            var list = _db.Cars.ToList();
            var lamdaList = list.Select(x => GetCarstResponse.ConvertToResponseFromDbModel(x)).ToList();
            return lamdaList;
        }

        public async Task<GetCarstResponse> AddAsync(CarsCreateDto request)
        {
            var item = CarsCreateDto.ConvertToDbModel(request);
            //  _db.cars.Add(item);
            //  _db.SaveChanges();
            await _db.Cars.AddAsync(item);    // async EF Core add
            await _db.SaveChangesAsync();     // async save
            //var response = GetCarstResponse.ConvertToResponseFromDbModel(item);
            var response = GetCarstResponse.ConvertToResponseFromDbModel(item);
            return response;
        }

        public GetCarstResponse UpdateAsync(int id, CarsUpdateDto request)
        {
            var dbItem = _db.Cars.Find(id);
            if (dbItem is null)
                return null;
            
            dbItem.Model = request.Model;
            dbItem.Color = request.Color;
            dbItem.Price = request.Price;
            dbItem.Details = request.Details;
            dbItem.Image = request.Image;
            dbItem.Date = request.Date;

            _db.Update(dbItem);
            _db.SaveChanges();
            return GetCarstResponse.ConvertToResponseFromDbModel(dbItem);

        }

        public bool DeleteAsync(int id)
        {
            var item = _db.Cars.Find(id);
            if (item == null)
            {
                return false;
            }
            _db.Cars.Remove(item);
            _db.SaveChanges();
            return true;
        }







        
                query = query.Where(c =>
                (c.Model != null && c.Model.ToLower().Contains(search.ToLower())) ||
                (c.Color != null && c.Color.ToLower().Contains(search.ToLower()))
 
        );
            }

            return await query
                .Select(c => new GetCarstResponse
                {
                    Id = c.Id,
                    Model = c.Model,
                    Color = c.Color,
                    Price = c.Price,
                    Date = c.Date,
                    Image = c.Image,
                    Details = c.Details
                })
                .ToListAsync();
        }

        Task<Responses.TrucksShop.GetCarstResponse> ICarService.AddAsync(CarsCreateDto request)
        {
            throw new NotImplementedException();
        }

        Task<bool> ICarService.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<GetCarstResponse>> GetListAsync(string? search)
        {
            // Assume _context is your EF Core DbContext
            var query = _context.Cars.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c => c.Model.Contains(search));
            }

            var result = await query
                .Select(c => new GetCarstResponse
                {
                    Model = c.Model,
                    Color = c.Color,
                    Price = c.Price,
                    Details=c.Details,
                    Id=c.Id,
                    Image=c.Image,
                    Date=c.Date
                })
                .ToListAsync();

            return result;
        }

        Task<Responses.TrucksShop.GetCarstResponse?> ICarService.UpdateAsync(int id, CarsUpdateDto request)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<GetCarstResponse>> ICarService.GetCarsAsync(string? search)
        {
            throw new NotImplementedException();
        }

        Task<GetCarstResponse?> ICarService.UpdateAsync(int id, CarsUpdateDto request)
        {
            throw new NotImplementedException();
        }
    }
}
*/

using CarsShop.Db;
using CarsShop.Interfeces.Db;
using CarsShop.RequestsDto.CarsShop;
using CarsShop.Responses.CarsShop;
using Microsoft.EntityFrameworkCore;

namespace CarsShop.Services
{
    public class CarService : ICarService
    {
        private readonly AppDbCar _context;

        public CarService(AppDbCar context)
        {
            _context = context;
        }

        public async Task<GetCarstResponse> AddAsync(CarsCreateDto request)
        {
            var entity = CarsCreateDto.ConvertToDbModel(request);
            await _context.Cars.AddAsync(entity);
            await _context.SaveChangesAsync();
            return GetCarstResponse.ConvertToResponseFromDbModel(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Cars.FindAsync(id);
            if (entity == null) return false;

            _context.Cars.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<GetCarstResponse>> GetCarsAsync(string? search)
        {
            var query = _context.Cars.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var lower = search.ToLower();
                query = query.Where(c =>
                    (c.Model != null && c.Model.ToLower().Contains(lower)) ||
                    (c.Color != null && c.Color.ToLower().Contains(lower))
                );
            }

            return await query
                .Select(c => new GetCarstResponse
                {
                    Id = c.Id,
                    Model = c.Model,
                    Color = c.Color,
                    Price = c.Price,
                    Date = c.Date,
                    Image = c.Image,
                    Details = c.Details
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<GetCarstResponse>> GetListAsync(string? search)
        {
            return await GetCarsAsync(search);
        }

        public async Task<GetCarstResponse?> UpdateAsync(int id, CarsUpdateDto request)
        {
            var entity = await _context.Cars.FindAsync(id);
            if (entity == null) return null;

            entity.Model = request.Model;
            entity.Color = request.Color;
            entity.Price = request.Price;
            entity.Details = request.Details;
            entity.Image = request.Image;
            entity.Date = request.Date;

            await _context.SaveChangesAsync();
            return GetCarstResponse.ConvertToResponseFromDbModel(entity);
        }
    }
}