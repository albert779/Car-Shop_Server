using CarsShop.Dto.Responses;

namespace CarsShop.Interfeces.Services
{
    public interface IMyRequestsService
    {
        Task<DashboardResponse> GetDashboardAsync(int userId);

    }
}