namespace CarsShop.Responses.API
{
    public abstract class APIResponse
    {
        public bool Success { get; }
        protected APIResponse(bool success)
            
        {
            Success = success;    
        }

    }



}