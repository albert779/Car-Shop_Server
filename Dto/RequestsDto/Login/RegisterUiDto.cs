

namespace CarsShop.Dto.RequestsDto.Login
{
    public class RegisterUiDto
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        // Expected format: yyyy-MM-dd
        public string BirthDate { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public int RoleId { get; set; }
    }
}
