/*
using CarsShop.Db.Models;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace CarsShop.Dto.RequestsDto.Login
{
    public class RegisterUiDto
    {
        //public int Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string BirthDate { get; set; }

        public string Phone { get; set; }
        public int RoleId { get; set; } 

        public User ConvertToDbModel()
        {
            var model = new User()
            {
                BirthDate = DateOnly.Parse(BirthDate),
                CreationDate = DateOnly.FromDateTime(DateTime.Now),
                Email = Email,
                LastName = LastName,
                FirstName = FirstName,
                //Id = Id,
                Phone = Phone,
                Password = string.Empty,
                //Password = Password,
                RoleId = RoleId
            };
            return model;
        }

    }
}
*/

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
