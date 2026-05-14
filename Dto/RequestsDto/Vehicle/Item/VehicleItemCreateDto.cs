using CarsShop.Db.Models;

namespace CarsShop.Dto.RequestsDto.Vehicle.Item
{
    public class VehicleItemCreateDto
    {
        public string? Color { get; set; }

        public string? Model { get; set; }
        public DateOnly Date { get; set; }
        public decimal Price { get; set; }
        public string? Details { get; set; }
        public string? Image { get; set; }
        public int VehicleTypeId { get; set; }
        

        public static Db.Models.Vehicle ConvertToDbModel(VehicleItemCreateDto item)
        {
           // var type = new VehicleType() { Id = item.VehicleTypeId };
            var date = item.Date.ToDateTime(TimeOnly.MinValue);
            var entety = new Db.Models.Vehicle()
            {
                Color = item.Color,
                Model = item.Model,
                Date = date,
                Price = item.Price,
                Details = item.Details,
                Image = item.Image,
                // VehicleType = type,

                VehicleTypeId = item.VehicleTypeId

            };
            return entety;
        }
    }
}
