using CarsShop.Db.Models;
using System.Runtime.CompilerServices;

namespace CarsShop.RequestsDto.Login
{
    public class RegisterDto
    {
        public int ID { get; set; } 
        public string Name { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public DateOnly BirthDate { get; set; }
        
        public string Phone { get; set; } = "";


        public User ConvertToDbModel()
        {
            var model = new User()
            {
                BirthDate = BirthDate,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Email = Email,
                LastName = LastName,
                Name = Name,
                ID = ID,
                Phone = Phone,
                Password = string.Empty
            };
            return model;
        }

    }
}