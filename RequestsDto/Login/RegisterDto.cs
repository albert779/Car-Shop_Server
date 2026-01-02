using CarsShop.Db.Models;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace CarsShop.RequestsDto.Login
{
    public class RegisterDto
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateOnly BirthDate { get; set; }
        
        public string Phone { get; set; }

        public User ConvertToDbModel()
        {
            var model = new User()
            {
                BirthDate = BirthDate,
                CreationDate = DateOnly.FromDateTime(DateTime.Now),
                Email = Email,
                LastName = LastName,
                Name = Name,
                Id = Id,
                Phone = Phone,
                Password = string.Empty,


                //BirthDate = DateOnly.ParseExact(
                //    BirthDate,
                //    "dd-MM-yyyy",
                //    CultureInfo.InvariantCulture
                //    ),
            };
            return model;
        }

    }
}