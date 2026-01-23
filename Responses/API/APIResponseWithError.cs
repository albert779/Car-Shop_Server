namespace CarsShop.Responses.API
{
    public class APIResponseWithError : APIResponse
    {
        private APIResponseWithError(string message) : base(false)
        {
            Message = message;
        }

        public string Message { get; }

        public static APIResponseWithError Create(string message)
        {
            return new APIResponseWithError(message);
        }
    }



}