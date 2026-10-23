/*
using CarsShop.Db;
using CarsShop.Db.Models;
using CarsShop.Dto.Messages;
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
               
                CreatedByUserId = request.UserId,

                
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

        Task<Message?> ISendRequestService.SendMessageAsync(CreateMessageDto dto, int senderId)
        {
            throw new NotImplementedException();
        }
    }
}
*/


using CarsShop.Db;
using CarsShop.Db.Models;
using CarsShop.Dto.Messages;
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
