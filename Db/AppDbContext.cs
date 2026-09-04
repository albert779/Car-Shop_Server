
/*
 * using CarsShop.Db.Models;
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
        //public object VehicleRequests { get; internal set; }
        public DbSet<VehicleRequest> VehicleRequests { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }


        // ✅ MUST be inside the class
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Vehicle>().ToTable("Vehicles");

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>()
                .HasKey(x => new { x.UserId, x.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.Role)
                .WithMany()
                .HasForeignKey(x => x.RoleId);
        }
    }
}
*/

using CarsShop.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace CarsShop.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<VehicleRequest> VehicleRequests { get; set; }
        public DbSet<RequestStatus> RequestStatuses { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserToRole> UserToRoles { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Vehicle
            modelBuilder.Entity<Vehicle>()
                .ToTable("Vehicles");

            // VehicleRequest
            modelBuilder.Entity<VehicleRequest>()
                .ToTable("VehicleRequest");

            modelBuilder.Entity<UserToRole>()
                .ToTable("UserToRole");

            // UserRole composite primary key
            modelBuilder.Entity<UserToRole>()
                .HasKey(x => new
                {
                    x.UserId,
                    x.RoleId
                });

            // UserRole -> User
            modelBuilder.Entity<UserToRole>()
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // UserRole -> Role
            modelBuilder.Entity<UserToRole>()
                .HasOne(x => x.Role)
                .WithMany()
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}