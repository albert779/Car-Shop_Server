using CarsShop.Db.Models;

namespace CarsShop.Responses.TrucksShop
{
    public class GetCarstResponse
    {
        public int Id { get; set; } // Add this property to fix CS0117

        public string? Color { get; set; }

        public string? Model { get; set; }

        public DateOnly Date { get; set; }
        public string? Details { get; set; }
        public decimal Price { get; set; }

        public string? Image { get; set; }

        public static GetCarstResponse ConvertToResponseFromDbModel(Truck dbItem)
        {
            var item = new GetCarstResponse()
            {
                Id = dbItem.Id,
                Color = dbItem.Color,
                Model = dbItem.Model,
                Date = dbItem.Date,
                Details = dbItem.Details,
                Price = dbItem.Price,
                Image = dbItem.Image
            };
            return item;
        }
    }
}
