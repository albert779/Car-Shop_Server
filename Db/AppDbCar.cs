using CarsShop.Db.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CarsShop.Db
{
    public class AppDbCar : DbContext
    {
        public AppDbCar(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Car>  Cars { get; set; }
    }
}
