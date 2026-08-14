using System.ComponentModel.DataAnnotations.Schema;

namespace CarsShop.Db.Models
{
    [Table("Vehicle")]
    public class Vehicle
    {
        public int Id { get; set; }
        public string? Color { get; set; }
        public string? Model { get; set; }
        public DateTime Date { get; set; }
        public string? Details { get; set; }
        public string? Image { get; set; }
        public decimal Price { get; set; }
        public VehicleType VehicleType { get; set; }
        public int VehicleTypeId { get; set; }
        public string ImageUrl { get; set; } = "";
    }
}
