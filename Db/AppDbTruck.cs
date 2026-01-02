using CarsShop.Db.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CarsShop.Db
{
    public class AppDbTruck : DbContext
    {
        public AppDbTruck(DbContextOptions<AppDbTruck> options) : base(options)
        {
        }

       

        public DbSet<Truck> Trucks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 👇 match the real table name in SQL Server
            modelBuilder.Entity<Truck>().ToTable("Trucks", "dbo");
        }
    }
}
