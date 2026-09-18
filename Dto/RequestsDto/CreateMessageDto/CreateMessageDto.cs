public class CreateMessageDto
{
    public int RequestId { get; set; }

    public int SenderId { get; set; }

    public int ReceiverId { get; set; }

    public string MessageText { get; set; } = string.Empty;
}