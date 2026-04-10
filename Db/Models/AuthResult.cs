namespace CarsShop.Responses.Auth
{
    public class AuthResult
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
        public static AuthResult GetResponseWithError(string errorMessage)
        {
            return new AuthResult(false, string.Empty, errorMessage);
        }

        // ✅ Factory method for success with token
        public static AuthResult GetResponseWithToken(string token, string firstName = "", string lastName = "", string email = "", string phone = "")
        {
            return new AuthResult(true, token, string.Empty)
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone
            };
        }

        // ✅ Private constructor
        private AuthResult(bool success, string token, string message)
        {
            this.Success = success;
            this.Token = token;
            this.Message = message;
        }
    }
}