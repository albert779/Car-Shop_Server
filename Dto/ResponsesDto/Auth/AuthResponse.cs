namespace CarsShop.Dto.Responses.Auth
{
    public class AuthResponse
    {
        // ✅ User info
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        // ✅ Status info
        public bool Success { get;  set; }
        public string Message { get;  set; } = string.Empty;
        public string Token { get;  set; } = string.Empty;

        // ✅ Factory method for error
        public static AuthResponse GetResponseWithError(string errorMessage)
        {
            return new AuthResponse(false, string.Empty, errorMessage);
        }

        // ✅ Factory method for success with token
        public static AuthResponse GetResponseWithToken(string token, string firstName = "", string lastName = "", string email = "", string phone = "")
        {
            return new AuthResponse(true, token, string.Empty)
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone
            };
        }

        // ✅ Private constructor
        private AuthResponse(bool success, string token, string message)
        {
            Success = success;
            Token = token;
            Message = message;
        }
    }
}