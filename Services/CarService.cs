using CarsShop.Db;
using CarsShop.Interfeces.Db;
using CarsShop.RequestsDto.CarsShop;
using CarsShop.Responses.CarsShop;
//using IDGCoreWebAPI.Controllers.CarsShop.Controllers;
using Microsoft.EntityFrameworkCore;

namespace CarsShop.Services
{

    public class CarService : ICarService
    {
        // private readonly ILogger<WeatherForecastController> _logger;
        private readonly AppDbCar _db;
        //private readonly AppDb _context;


        public CarService(AppDbCar db)
        {
            this._db = db;

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
    }
}
