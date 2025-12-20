using CarsShop.Db.Models;

namespace CarsShop.Responses.CarsShop
{
    public class GetCarstResponse
    {
        public int Id { get; set; }
        public string? Color { get; set; }

        public string? Model { get; set; }

        public DateOnly Date { get; set; }
        public decimal Price { get; set; }
        public string Details { get; set; }
        public string Image { get; set; }

        public static GetCarstResponse ConvertToResponseFromDbModel(Car dbItem)
        {
            var item = new GetCarstResponse()
            {
                Color = dbItem.Color,
                Model = dbItem.Model,
                Date = dbItem.Date,
                Details = dbItem.Details ?? string.Empty,
                Image = dbItem.Image ?? string.Empty,
                Price = dbItem.Price,
                Id = dbItem.Id
            };
            return item;
        }
    }
}
