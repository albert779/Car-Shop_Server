using CarsShop.Db;
using CarsShop.Interfeces.Db;
using CarsShop.RequestsDto.TrucksShop;
using CarsShop.RequestsDto.CarsShop;
using CarsShop.Responses.TrucksShop;
using IDGCoreWebAPI.Controllers.CarsShop.Controllers;
using Microsoft.EntityFrameworkCore;

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
    }
}
