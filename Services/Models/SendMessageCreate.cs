using System.ComponentModel.DataAnnotations;

namespace CarsShop.Services.Models
{
    public class SendMessageCreate
    {
        public int RequestId { get; set; }

        public string MessageText { get; set; }
        public int SenderId { get; set; }

        public SendMessageCreate(int requestId, string messageText, int senderId)
        {

            if (requestId <= 0)
            {
                throw new InvalidOperationException(
                    "Request owner ID is invalid.");
            }

            if (senderId <= 0)
            {
                throw new InvalidOperationException(
                    "Sender ID is invalid.");

            }
            if (string.IsNullOrWhiteSpace(messageText))
            {
                throw new ArgumentException(
                    "Message text is required.");
            }


            this.MessageText = messageText.Trim();
            this.RequestId = requestId;
            this.SenderId = senderId;
           
        }
    }
}
