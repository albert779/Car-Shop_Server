using CarsShop.RequestsDto.Vehicle.Item;
using CarsShop.Responses.CarsShop;

namespace CarsShop.Interfeces.Db
{
    public interface IVehicleService
    {
        Task<GetVehicleResponse> AddAsync(VehicleItemCreateDto request);

        Task<bool> DeleteAsync(int id);

        // Get all trucks as response DTOs
        Task<IEnumerable<GetVehicleResponse>> GetListAsync(string? search);
        //Task<IEnumerable<GetVehicleResponse>> GetListAsync();

        Task<GetVehicleResponse?> GetByIdAsync(int id);

        //Task<IEnumerable<GetVehicleResponse>> GetListAsync(string? search);

        Task<GetVehicleResponse?> UpdateAsync(int vehicleId, VehicleItemUpdateDto request);
    }
}