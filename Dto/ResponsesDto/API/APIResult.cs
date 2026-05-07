namespace CarsShop.Dto.Responses.API
{
    public abstract class APIResult
    {
        public bool Success { get; }
        protected APIResult(bool success)
            
        {
            Success = success;    
        }

    }

}