using CarsShop.Db;
using CarsShop.Interfeces.Db;
using CarsShop.RequestsDto.TrucksShop;
using CarsShop.Responses.TrucksShop;
//using IDGCoreWebAPI.Controllers.CarsShop.Controllers;
using Microsoft.EntityFrameworkCore;
using CarsShop.Db.Models;

/*

namespace CarsShop.Services
{

   public class TruckService : ITruckService
   {



       // private readonly ILogger<WeatherForecastController> _logger;
       private readonly AppDbTruck _db;
       //private readonly AppDb _context;

       public TruckService(AppDbTruck db)
       {
           this._db = db;

       }

       public IEnumerable<GetCarstResponse> GetList()
       {
           var dbName = _db.Database.GetDbConnection().Database;
           Console.WriteLine($"Connected to database: {dbName}");

           var list = _db.Trucks.ToList();
           var lamdaList = list.Select(x => GetCarstResponse.ConvertToResponseFromDbModel(x)).ToList();
           return lamdaList;
       }

       public GetCarstResponse AddAsync(TrucksCreateDto request)
       { 
           var item = TrucksCreateDto.ConvertToDbModel(request);
           _db.Trucks.Add(item);
           _db.SaveChanges();
           var response = GetCarstResponse.ConvertToResponseFromDbModel(item);
           return response;
       }

       public GetCarstResponse? UpdateAsync(int id, TrucksUpdateDto request)
       {
           var dbItem = _db.Trucks.Find(id);
           if (dbItem != null)
           {
               dbItem.Model = request.Model;
               dbItem.Color = request.Color;
               dbItem.Price = request.Price;     // ✅ ADD THIS LINE
               dbItem.Details = request.Details; // ✅ if applicable
               dbItem.Image = request.Image;
               dbItem.Date = request.Date;

               _db.Update(dbItem);
               _db.SaveChanges();
               return GetCarstResponse.ConvertToResponseFromDbModel(dbItem);
           }
           return null;
       }

       public bool DeleteAsync(int id)
       {
           var item = _db.Trucks.Find(id);
           if (item == null)
           {
               return false;
           }
           _db.Trucks.Remove(item);
           _db.SaveChanges();
           return true;
       }

       //public Task<Truck> CreateAsync(TruckDto dto)
       //{
       //    throw new NotImplementedException();
       //}

       //public Task<Truck> GetByIdAsync(int id)
       //{
       //    throw new NotImplementedException();
       //}

       //public Task<bool> UpdateAsync(int id, TruckDto dto)
       //{
       //    throw new NotImplementedException();
       //}

       //public Task<List<Truck>> GetListAsync()
       //{
       //    throw new NotImplementedException();
       //}

       //public Task<Truck> CreateAsync(TruckDto dto)
       //{
       //    throw new NotImplementedException();
       //}

       //public Task<bool> UpdateAsync(int id, TruckDto dto)
       //{
       //    throw new NotImplementedException();
       //}
   }
}


       using CarsShop.Db;
using CarsShop.Interfeces.Db;
using CarsShop.RequestsDto.TrucksShop;
using CarsShop.Responses.TrucksShop;
using CarsShop.Db.Models;
using Microsoft.EntityFrameworkCore;
        */
namespace CarsShop.Services
{
    public class TruckService : ITruckService
    {
        private readonly AppDbTruck _db;

        public TruckService(AppDbTruck db)
        {
            _db = db;
        }

        // CREATE
        public async Task<Truck> CreateAsync(TrucksCreateDto dto)
        {
            var entity = new Truck
            {
                Model = dto.Model,
                Color = dto.Color,
                Price = dto.Price,
                Details = dto.Details,
                Image = dto.Image,
                Date = dto.Date
            };

            await _db.Trucks.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        // GET BY ID
        public async Task<Truck?> GetByIdAsync(int id)
        {
            return await _db.Trucks.FindAsync(id);
        }

        // UPDATE
        public async Task<bool> UpdateAsync(int id, TrucksUpdateDto dto)
        {
            var entity = await _db.Trucks.FindAsync(id);
            if (entity == null) return false;

            entity.Model = dto.Model;
            entity.Color = dto.Color;
            entity.Price = dto.Price;
            entity.Details = dto.Details;
            entity.Image = dto.Image;
            entity.Date = dto.Date;

            _db.Trucks.Update(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        // DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _db.Trucks.FindAsync(id);
            if (entity == null) return false;

            _db.Trucks.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        // GET ALL
        public async Task<IEnumerable<GetCarstResponse>> GetListAsync()
        {
            var trucks = await _db.Trucks.ToListAsync();
            return trucks.Select(GetCarstResponse.ConvertToResponseFromDbModel);
        }
    }
}