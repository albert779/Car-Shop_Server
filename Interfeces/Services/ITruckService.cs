
using CarsShop.Controllers;
using CarsShop.Db.Models;
using CarsShop.RequestsDto.CarsShop;
using CarsShop.RequestsDto.TrucksShop;
using CarsShop.Responses.TrucksShop;


namespace CarsShop.Interfeces.Db
{
    public interface ITruckService
    {
        //Task<List<Truck>> GetListAsync();
        // Create a truck
        Task<Truck> CreateAsync(TrucksCreateDto dto);

        // Get a truck by ID
        Task<Truck?> GetByIdAsync(int id);

        // Update a truck by ID
        Task<bool> UpdateAsync(int id, TrucksUpdateDto dto);

        // Delete a truck by ID
        Task<bool> DeleteAsync(int id);

        // Get all trucks as response DTOs
        Task<IEnumerable<GetCarstResponse>> GetListAsync();

       
    }
}
