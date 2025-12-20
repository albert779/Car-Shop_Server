using CarsShop.Db.Models;

namespace CarsShop.RequestsDto.CarsShop
{
    public class CarsCreateDto
    {
        public string? Color { get; set; }

        public string? Model { get; set; }
        public DateOnly Date { get; set; }
        public decimal Price { get; set; }
        public string? Details { get; set; }
        public string? Image { get; set; }

        public static Car ConvertToDbModel(CarsCreateDto item)
        {
            var entety = new Car()
            {
                Color = item.Color ?? "",
                Model = item.Model ?? "",
                Date = item.Date,
                Price = item.Price,
                Details = item.Details ?? "",
                Image = item.Image ?? ""

            };
            return entety;
        }
    }
}
