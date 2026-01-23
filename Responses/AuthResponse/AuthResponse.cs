namespace CarsShop.Responses.Auth
{
    public class AuthResponse
    {
        public bool Success { get; }
        public string Message { get; }
        public string Token { get; }

        public static AuthResponse GetResponseWithError(string erorrMessage)
        {
            return new AuthResponse(false, string.Empty, erorrMessage);
        }

        public static AuthResponse GetResponseWithToken(string token)
        {
            return new AuthResponse(true, token, string.Empty);
        }

        private AuthResponse(bool success, string token, string message)
        {
            this.Token = token;
            this.Success = success;
            this.Message = message;
        }
    }



}