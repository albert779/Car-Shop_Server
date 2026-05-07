using CarsShop.Db.Models;
using System;

namespace CarsShop.Dto.Responses.VehicleShop
{
    public class GetVehicleResponse
    {
        public int Id { get; set; }
        public string? Color { get; set; }

        public string? Model { get; set; }

        public DateOnly Date { get; set; }
        public decimal Price { get; set; }
        public string Details { get; set; }
        public string Image { get; set; }
        public string VehicleType { get; internal set; }

        public static GetVehicleResponse ConvertToResponseFromDbModel(Vehicle dbItem)
        {
            var item = new GetVehicleResponse()
            {
                Color = dbItem.Color,
                Model = dbItem.Model,
                Date = DateOnly.FromDateTime(dbItem.Date),
                Details = dbItem.Details ?? string.Empty,
                Image = dbItem.Image ?? string.Empty,
                Price = dbItem.Price,
                Id = dbItem.Id
            };
            return item;
        }
    }
}
