
using CarsShop.Db.Models;
using CarsShop.Dto.RequestsDto;
using CarsShop.Dto.Responses;
using CarsShop.Dto.Responses.VehicleRequests;
using CarsShop.Dto;
using CarsShop.Dto.Responses;


namespace CarsShop.Interfeces.Services
{
    public interface IVehicleRequestService
    {
        Task AddNew(VehicleRequestCreateDto request, int userIdRequested);
        Task<DashboardResponse> GetDashboardAsync(int userId);
        Task<PagedResult<VehicleRequestResponse>> GetRequestsAsync(
            int userId,
            RequestFilterDto filter);
    }
}
