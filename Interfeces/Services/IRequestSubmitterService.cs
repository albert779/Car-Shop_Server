using CarsShop.Db.Models;

namespace CarsShop.Interfeces.Services
{
    public interface IRequestSubmitterService
    {
        Task<VehicleRequest> AddNew(VehicleRequestCreateDto request, int userIdRequested);
        //Task AddNew(CreateRequestDto request);
    }
}
