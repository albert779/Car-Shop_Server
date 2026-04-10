using System;

namespace CarsShop.Db.Models
{
    public class TruckRequestInfo
    {
        public int Id { get; set; }
        public int TruckId { get; set; }        // Link to Truck
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}