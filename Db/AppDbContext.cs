
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

            // =========================
            // Vehicle
            // =========================
            modelBuilder.Entity<Vehicle>()
                .ToTable("Vehicles");

            // =========================
            // VehicleRequest
            // =========================
            modelBuilder.Entity<VehicleRequest>()
                .ToTable("VehicleRequest");

            // =========================
            // UserToRole
            // =========================
            modelBuilder.Entity<UserToRole>()
                .ToTable("UserToRole");

            modelBuilder.Entity<UserToRole>()
                .HasKey(x => new
                {
                    x.UserId,
                    x.RoleId
                });

            // UserToRole -> User
            modelBuilder.Entity<UserToRole>()
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // UserToRole -> Role
            modelBuilder.Entity<UserToRole>()
                .HasOne(x => x.Role)
                .WithMany()
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            // =========================
            // Message
            // =========================
            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Messages");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.RequestId)
                    .HasColumnName("RequestId")
                    .IsRequired();

                entity.Property(x => x.CreatedByUserId)
                    .HasColumnName("CreatedByUserId")
                    .IsRequired();

                entity.Property(x => x.ReplyedByUserId)
                    .HasColumnName("ReplyedByUserId")
                    .IsRequired();

                entity.Property(x => x.MessageText)
                    .HasColumnName("MessageText")
                    .IsRequired();

                entity.Property(x => x.CreatedAt)
                    .HasColumnName("CreatedAt")
                    .IsRequired();

                // Message -> VehicleRequest
                entity.HasOne(x => x.Request)
                    .WithMany(x => x.Messages)
                    .HasForeignKey(x => x.RequestId)
                    .HasPrincipalKey(x => x.Id)
                    .OnDelete(DeleteBehavior.Restrict);

               

                entity.HasOne(x => x.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(x => x.CreatedByUserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.ReplyedByUser)
                    .WithMany()
                    .HasForeignKey(x => x.ReplyedByUserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}