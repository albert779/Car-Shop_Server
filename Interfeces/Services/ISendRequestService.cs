using CarsShop.Db.Models;
using CarsShop.Dto.Requests;

namespace CarsShop.Interfeces.Services
{
    public interface ISendRequestService
    {
        Task<Message?> SendMessageAsync(
            CreateMessageDto dto,
            int senderId);
    }
}