using CarsShop.Db.Models;

namespace CarsShop.RequestsDto.TrucksShop
{
    public class TrucksCreateDto
    {
        public string Details;

        public string? Color { get; set; }

        public string? Model { get; set; }

        public DateOnly Date { get; set; }
        public decimal Price { get; set; } // e.g., 20.5 tons
        public string? Image { get; set; }

        public static Truck ConvertToDbModel(TrucksCreateDto item)
        {
            var entety = new Truck()
            {
                Color = item.Color,
                Model = item.Model,
                Price = item.Price,
                Details = item.Details,
                Image = item.Image,
                Date = item.Date
            };
            return entety;
        }
    }
}
