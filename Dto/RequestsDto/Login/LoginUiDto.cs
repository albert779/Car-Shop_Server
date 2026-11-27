using CarsShop.Models.Requests;
using System.ComponentModel.DataAnnotations;

namespace CarsShop.Dto.RequestsDto.Login
{
    public class LoginUiDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }


        public static LoginBl ConvertToServiceLogin(LoginUiDto input)
        {
            return new LoginBl
            {
                Email = input.Email,
                Password = input.Password
            };
        }
    }
}