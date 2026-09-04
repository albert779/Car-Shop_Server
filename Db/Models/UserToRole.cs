using CarsShop.Db.Models;
using System.Data;

namespace CarsShop.Db.Models
{
    public class UserToRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public User User { get; set; } = null!;
        public Role Role { get; set; } = null!;
    }
}