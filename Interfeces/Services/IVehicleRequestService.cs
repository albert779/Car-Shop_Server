using CarsShop.Db.Models;

namespace CarsShop.Interfeces.Services
{
    public interface IVehicleRequestService
    {
        Task AddNew(VehicleRequestCreateDto request, int userIdRequested);
    }
}
