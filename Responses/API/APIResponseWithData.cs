
/*
using CarsShop.Responses.TrucksShop;

namespace CarsShop.Responses.API
{
    public class APIResponseWithData<T> : APIResponse
    {
        public T Data { get; }
      
        private APIResponseWithData(T data): base (true)
        {
            Data = data;
        }
        public static APIResponseWithData<T> Create(T data)
        {
            return new APIResponseWithData<T>(data);
        }

        internal static object? Create(GetCarstResponse createdCar)
        {
            throw new NotImplementedException();
        }

        internal static object? Create(Task<GetCarstResponse?> updatedCar)
        {
            throw new NotImplementedException();
        }


        /*
        internal static object? Create(Task<CarsShop.GetCarstResponse?> updatedCar)
        {
            //throw new NotImplementedException();
            return updatedCar;
        }
        */

using CarsShop.Responses.TrucksShop;

namespace CarsShop.Responses.API
{
    public class APIResponseWithData<T> : APIResponse
    {
        public T Data { get; }

        private APIResponseWithData(T data) : base(true)
        {
            Data = data;
        }

        // ✅ Generic factory method
        public static APIResponseWithData<T> Create(T data)
        {
            return new APIResponseWithData<T>(data);
        }

        // ❌ Remove all Task-based or NotImplemented Create methods
        // They are causing serialization errors

        // Example: remove or comment out
        // internal static object? Create(GetCarstResponse createdCar) { throw new NotImplementedException(); }
        // internal static object? Create(Task<GetCarstResponse?> updatedCar) { throw new NotImplementedException(); }

        // Optional: if you want a convenience method for GetCarstResponse
        // just use the generic Create<T> method instead

        // ✅ Correct usage in controller:
        // var updatedCar = await _carService.UpdateAsync(id, request);
        // return Ok(APIResponseWithData<GetCarstResponse>.Create(updatedCar));
    }
}



