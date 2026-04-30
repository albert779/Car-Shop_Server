namespace CarsShop.Db.Models
{
    public class VehicleRequest
    {
        public int Id { get; set; }
        public Vehicle Vehicle { get; set; }
        public string Message { get; set; } = string.Empty;
        public User User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdate { get; set; } = DateTime.UtcNow;
        public RequestStatus Status { get; set; }
    }
}