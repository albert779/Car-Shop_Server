using CarsShop.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace CarsShop.Db
{
    public class AppDbContext : DbContext
    {
        public DbSet<VehicleRequest> VehicleRequest { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        //public DbSet<VehicleRequest> CarInfoRequests { get; set; }
        public DbSet<VehicleRequest> TruckRequestInfos { get; set; }
        public DbSet<RequestStatus> RequestStatuses { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<User> Users { get; set; }
        

        // ✅ MUST be inside the class
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Vehicle>().ToTable("Vehicles");
        }
    }
}