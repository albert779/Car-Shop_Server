using CarsShop.Db.Models;
using CarsShop.Dto.RequestsDto.Vehicle.Item;
using CarsShop.Dto.Responses.VehicleShop;

namespace CarsShop.Interfeces.Db
{
    public interface IVehicleRequestService
    {

        Task<VehicleRequest> CreateAsync(VehicleRequestCreateDto dto, int userId);
        Task<GetVehicleResponse> AddAsync(VehicleItemCreateDto request);

        Task<bool> DeleteAsync(int id);

        // Get all trucks as response DTOs
        Task<IEnumerable<GetVehicleResponse>> GetListAsync(string? search);

        Task<GetVehicleResponse> GetByIdAsync(int id);

        //Task<IEnumerable<GetVehicleResponse>> GetListAsync(string? search);
        Task<GetVehicleResponse?> UpdateAsync(int vehicleId, VehicleItemUpdateDto request);
        Task<List<GetVehicleResponse>> SearchAsync(string? text);
    }
}