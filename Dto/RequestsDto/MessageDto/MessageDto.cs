namespace CarsShop.Dto.Messages
{
    public class MessageDto
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string MessageText { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}