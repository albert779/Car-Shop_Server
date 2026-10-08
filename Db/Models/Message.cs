
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

        // User who created the original vehicle request
        public int CreatedByUserId { get; set; }

        // User who replied to the request
        public int ReplyedByUserId { get; set; }

        [Required]
        public string MessageText { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        // Message -> VehicleRequest
        [ForeignKey(nameof(RequestId))]
        public VehicleRequest Request { get; set; } = null!;

        // Message -> original requester
        [ForeignKey(nameof(CreatedByUserId))]
        public User CreatedByUser { get; set; } = null!;

        // Message -> person who replied
        [ForeignKey(nameof(ReplyedByUserId))]
        public User ReplyedByUser { get; set; } = null!;
    }
}

