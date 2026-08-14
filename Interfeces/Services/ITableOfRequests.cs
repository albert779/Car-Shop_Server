using CarsShop.Db.Models;
using CarsShop.Dto.Responses.VehicleRequests;

namespace CarsShop.Interfeces.Services
{
    public interface ITableOfRequests
    {
        Task<IEnumerable<VehicleRequestResponse>> GetRequests(string? search);
    }
}