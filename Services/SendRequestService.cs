using CarsShop.Database;
using CarsShop.Database.Models;
using CarsShop.Interfeces.Services;
using CarsShop.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace CarsShop.Services
{
    public class SendRequestService : ISendRequestService
    {
        private readonly AppDbContext _context;

        public SendRequestService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Message> SendMessageAsync(SendMessageCreate message)
        {

            var request = await _context.VehicleRequests.SingleAsync(x => x.Id == message.RequestId);

            // The owner of the original vehicle request.
            var requestOwnerId = request.UserId;

            var newMessage = new Message
            {
                RequestId = request.Id,

                // The original request owner.
                CreatedByUserId = requestOwnerId,

                // The user sending this message or reply.
                ReplyedByUserId = message.SenderId,

                MessageText = message.MessageText,
                CreatedAt = DateTime.UtcNow,
                Request = request
            };

            _context.Messages.Add(newMessage);
            request.LastUpdate = newMessage.CreatedAt;

            await _context.SaveChangesAsync();

            return newMessage;
        }
    }
}
