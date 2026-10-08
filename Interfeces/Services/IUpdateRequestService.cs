using CarsShop.Dto.Requests;
using CarsShop.Db.Models;

namespace CarsShop.Interfeces.Services
{
    public interface IUpdateRequestService
    {
        Task<VehicleRequest?> UpdateRequestAsync(
            int id,
            UpdateRequestDto dto);
    }
}