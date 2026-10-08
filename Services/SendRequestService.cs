using CarsShop.Db;
using CarsShop.Db.Models;
using CarsShop.Dto.Requests;
using CarsShop.Interfeces.Services;
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

        public async Task<Message?> SendMessageAsync(
            CreateMessageDto dto,
            int senderId)
        {
            var request = await _context.VehicleRequests
                .FirstOrDefaultAsync(x => x.Id == dto.RequestId);

            if (request == null)
            {
                return null;
            }

            var receiverId = request.UserId;

            if (receiverId <= 0)
            {
                throw new InvalidOperationException(
                    "Receiver ID is invalid.");
            }

            var now = DateTime.UtcNow;

            var newMessage = new Message
            {
                RequestId = request.Id,
                /*
                SenderId = senderId,
                ReceiverId = receiverId,
                */
                // User who created the original vehicle request
                CreatedByUserId = request.UserId,

                // Manager or user sending this reply
                ReplyedByUserId = senderId,
                MessageText = dto.MessageText.Trim(),
                CreatedAt = now,
                Request = request
            };

            _context.Messages.Add(newMessage);

            request.LastUpdate = now;

            await _context.SaveChangesAsync();

            return newMessage;
        }
    }
}