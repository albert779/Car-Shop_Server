using CarsShop.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace CarsShop.Db
{
    public class AppDbUser : DbContext
    {
        public AppDbUser(DbContextOptions options)
            : base(options)
        {
        }

        // This table will store users who register
        public DbSet<User> Users { get; set; }
    }
}