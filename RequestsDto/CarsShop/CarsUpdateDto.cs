using CarsShop.Db.Models;

namespace CarsShop.RequestsDto.CarsShop
{
    public class CarsUpdateDto
    {
        public string? Color { get; set; }

        public string? Model { get; set; }
        public decimal Price { get; set; }
        public string? Details { get; set; }
        public string? Image { get; set; }
        public DateOnly Date { get; set; }


        public static Car ConvertToDbModel(CarsUpdateDto item)
        {
            var entety = new Car()
            {
                Color = item.Color,
                Model = item.Model,
                Price = item.Price,
                Details=item.Details,
                Image=item.Image,
                Date = item.Date
            };
            return entety;
        }

    }
}
