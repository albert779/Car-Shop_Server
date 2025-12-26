namespace CarsShop.Db.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public DateOnly BirthDate { get; set; }
        public DateOnly CreationDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string Phone { get; set; } = "";
    }
}
