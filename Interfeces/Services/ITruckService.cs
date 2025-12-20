using CarsShop.RequestsDto.CarsShop;
using CarsShop.RequestsDto.TrucksShop;
using CarsShop.Responses.TrucksShop;



namespace CarsShop.Interfeces.Db
{

    public interface ITruckService
    {
        GetCarstResponse AddAsync(TrucksCreateDto request);
        bool DeleteAsync(int id);
        IEnumerable<GetCarstResponse> GetList();
        GetCarstResponse UpdateAsync(int id, TrucksUpdateDto request);
    }
}
