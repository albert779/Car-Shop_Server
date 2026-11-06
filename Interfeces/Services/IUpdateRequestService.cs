using CarsShop.Database.Models;
using CarsShop.Dto.Requests;

namespace CarsShop.Interfeces.Services
{
    public interface IUpdateRequestService
    {
        Task<VehicleRequest?> UpdateRequestAsync(
            int id,
            UpdateRequestDto dto);
    }
}