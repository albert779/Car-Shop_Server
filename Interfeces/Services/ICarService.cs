using CarsShop.RequestsDto.CarsShop;
using CarsShop.Responses.CarsShop;

namespace CarsShop.Interfeces.Db
{

    public interface ICarService
    {
        Task<GetCarstResponse> AddAsync(CarsCreateDto request);
        bool DeleteAsync(int id);
        IEnumerable<GetCarstResponse> GetList();
        GetCarstResponse UpdateAsync(int id, CarsUpdateDto request);
    }
}
