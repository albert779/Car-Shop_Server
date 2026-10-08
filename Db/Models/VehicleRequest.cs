/*
namespace CarsShop.Db.Models
{
    public class VehicleRequest
    {
        public int Id { get; set; }
        public Vehicle Vehicle { get; set; }

        public int VehicleId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; } = string.Empty;
        public User User { get; set; }
        public int RequestStatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdate { get; set; } = DateTime.UtcNow;
        public RequestStatus Status { get; set; }
        public ICollection<Message> Messages { get; set; }
    = new List<Message>();
    }
}
*/

namespace CarsShop.Db.Models
{
    public class VehicleRequest
    {
        public int Id { get; set; }

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public string Message { get; set; } = string.Empty;

        public int RequestStatusId { get; set; }
        public RequestStatus Status { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdate { get; set; } = DateTime.UtcNow;

        // Messages belonging to this request
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}