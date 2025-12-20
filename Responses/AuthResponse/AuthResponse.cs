namespace CarsShop.Responses.Auth
{
    public class AuthResponse
    {
        public bool Success { get; }
        public string Message { get;}
        public string Token { get;}

        public AuthResponse(bool success,string token): this(success, token,string.Empty )
        {

        }

        public AuthResponse(bool success, string token, string message)
        {
            this.Token = token;
            this.Success = success;
            this.Message = message;
        }
    }
}