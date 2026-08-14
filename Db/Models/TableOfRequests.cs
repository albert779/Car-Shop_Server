namespace CarsShop.Db.Models
{
    public class TableOfRequests
    {
        public int Id { get; set; }

        public string Vehicle { get; set; } = "";

        public string Image { get; set; } = "";

        public string Message { get; set; } = "";

        public string Status { get; set; } = "";

        public DateTime RequestedOn { get; set; }

        public DateTime LastUpdate { get; set; }

        public string FirstName { get; set; } = "";

        public string LastName { get; set; } = "";

        public string Email { get; set; } = "";

        public string Phone { get; set; } = "";
    }
}
