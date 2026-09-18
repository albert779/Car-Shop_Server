


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
        public DbSet<Message> Messages { get; set; }

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


            // =========================
            // Message
            // =========================
            modelBuilder.Entity<Message>()
                .ToTable("Messages");

            // Message -> VehicleRequest
            modelBuilder.Entity<Message>()
                .HasOne(x => x.Request)
                .WithMany()
                .HasForeignKey(x => x.RequestId)
                .OnDelete(DeleteBehavior.Restrict);

            // Message -> User (Sender)
            modelBuilder.Entity<Message>()
                .HasOne(x => x.Sender)
                .WithMany()
                .HasForeignKey(x => x.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            // Message -> User (Receiver)
            modelBuilder.Entity<Message>()
                .HasOne(x => x.Receiver)
                .WithMany()
                .HasForeignKey(x => x.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}