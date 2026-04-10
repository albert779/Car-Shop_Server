

namespace CarsShop.Responses.Auth
{
    public class AuthResponse
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public bool Success { get;  set; }
        public string Message { get;  set; } = string.Empty; // Optional success/failure message
        public string Token { get;  set; } = string.Empty;
        public string Error { get;  set; } = string.Empty;   // Error message

        // ✅ Factory method for error
        public static AuthResponse GetResponseWithError(string errorMessage)
        {
            return new AuthResponse
            {
                Success = false,
                Error = errorMessage
            };
        }

        // ✅ Factory method for success
        public static AuthResponse GetResponseWithToken(
            string token,
            string firstName = "",
            string lastName = "",
            string email = "",
            string phone = ""
        )
        {
            return new AuthResponse
            {
                Success = true,
                Token = token,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone
            };
        }

        // Private constructor ensures factory methods are used
        public AuthResponse() { }
    }
}