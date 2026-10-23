using System.ComponentModel.DataAnnotations;

namespace CarsShop.Dto.Messages
{
    public class CreateMessageDto
    {
        [Required]
        public int RequestId { get; set; }

        [Required]
        [StringLength(2000)]
        public string MessageText { get; set; } = string.Empty;
    }
}