using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsShop.Db.Models
{
    [Table("Messages")]
    public class Message
    {
        [Key]
        public int Id { get; set; }

        public int RequestId { get; set; }

        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        [Required]
        public string MessageText { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        // Navigation
        [ForeignKey(nameof(RequestId))]
        public VehicleRequest? Request { get; set; }

        [ForeignKey(nameof(SenderId))]
        public User? Sender { get; set; }

        [ForeignKey(nameof(ReceiverId))]
        public User? Receiver { get; set; }
        //public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}