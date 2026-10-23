using System;
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

        public int CreatedByUserId { get; set; }

        public int ReplyedByUserId { get; set; }

        [Required]
        public string MessageText { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey(nameof(RequestId))]
        public virtual VehicleRequest? Request { get; set; }

        [ForeignKey(nameof(CreatedByUserId))]
        public virtual User? Sender { get; set; }

        [ForeignKey(nameof(ReplyedByUserId))]
        public virtual User? Receiver { get; set; }
    }
}