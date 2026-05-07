using CarsShop.Db.Models;

namespace CarsShop.Dto.RequestsDto.Vehicle.Item
{
    public class VehicleItemUpdateDto
    {
        public string? Color { get; set; }
        public string? Model { get; set; }
        public decimal Price { get; set; }
        public string? Details { get; set; }
        public string? Image { get; set; }
        public DateOnly Date { get; set; }


    }
}
