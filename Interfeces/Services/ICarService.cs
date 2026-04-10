using CarsShop.Db.Models;
using CarsShop.RequestsDto.CarsShop;
using CarsShop.Responses.CarsShop;
using CarsShop.Responses.TrucksShop;
using GetCarstResponse = CarsShop.Responses.CarsShop.GetCarstResponse;

namespace CarsShop.Interfeces.Db
{
/*
    public interface ICarService
    {
        
        Task<GetCarstResponse> AddAsync(CarsCreateDto request);
        bool DeleteAsync(int id);
        IEnumerable<GetCarstResponse> GetList();
        GetCarstResponse UpdateAsync(int id, CarsUpdateDto request);
        Task<List<Car>> GetCarsAsync(string? search);
        Task<IEnumerable<GetCarsResponse>> GetListAsync(string? search);
        */
        /*
        Task<GetCarstResponse> AddAsync(CarsCreateDto request);

        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<GetCarstResponse>> GetCarsAsync(string? search);
        Task<IEnumerable<Responses.CarsShop.GetCarstResponse>> GetListAsync(string? search);
        Task<GetCarstResponse?> UpdateAsync(int id, CarsUpdateDto request);
        */



        public interface ICarService
        {
            /// <summary>
            /// Add a new car
            /// </summary>
            Task<GetCarstResponse> AddAsync(CarsCreateDto request);

            /// <summary>
            /// Delete a car by ID
            /// </summary>
            Task<bool> DeleteAsync(int id);

            /// <summary>
            /// Get cars filtered by search string (returns list)
            /// </summary>
            Task<IEnumerable<GetCarstResponse>> GetCarsAsync(string? search);

            /// <summary>
            /// Get all cars (optionally filtered)
            /// </summary>
            Task<IEnumerable<GetCarstResponse>> GetListAsync(string? search);

            /// <summary>
            /// Update an existing car
            /// </summary>
            Task<GetCarstResponse?> UpdateAsync(int id, CarsUpdateDto request);
        }
    }


