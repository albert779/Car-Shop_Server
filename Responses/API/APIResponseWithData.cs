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


    }



}