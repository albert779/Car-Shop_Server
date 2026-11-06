using CarsShop.Database.Models;

using CarsShop.Dto.Messages;
using CarsShop.Services.Models;

namespace CarsShop.Interfeces.Services
{
    public interface ISendRequestService
    {
        Task<Message?> SendMessageAsync(SendMessageCreate messageCreate);
    }
}