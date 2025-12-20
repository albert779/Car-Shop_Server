using CarsShop.Db.Models;

namespace CarsShop.RequestsDto.TrucksShop
{
    public class TrucksUpdateDto
    {
        public string? Color { get; set; }

        public string? Model { get; set; }

        public decimal Price { get; set; }

        public string? Details { get; set; }
        public string? Image { get; set; }
        public DateOnly Date { get; set; }


        public static Truck ConvertToDbModel(TrucksUpdateDto item)
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
