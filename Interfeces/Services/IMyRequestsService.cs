using CarsShop.Dto.RequestsDto;
using CarsShop.Dto.Responses;

namespace CarsShop.Interfeces.Services
{
    public interface IMyRequestsService
    {
        Task<DashboardResponse> GetDashboardAsync(int userId);

        Task<MyRequestsPageDto> GetMyRequestsAsync(
            int userId,
            int page,
            int pageSize);


    }
}