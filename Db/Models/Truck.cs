namespace CarsShop.Db.Models
{
    public class Truck
    {
        public int Id { get; set; }

        public string? Color { get; set; }

        public string? Model { get; set; }

        public DateOnly Date { get; set; }
        public decimal Price { get; set; }
        public string? Details { get; set; }
        public string? Image { get; set; }
    }
}
