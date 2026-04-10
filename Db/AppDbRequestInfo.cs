/*
using CarsShop.Db.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CarsShop.Db
{
    public class AppDbRequestInfo : DbContext
    {
        public AppDbRequestInfo(DbContextOptions<AppDbRequestInfo> options) : base(options) { }

        public DbSet<CarInfoRequest> CarInfoRequests { get; set; }
        public DbSet<TruckRequestInfo> TruckRequestInfos { get; set; }  // Added
    }
}


*/

using CarsShop.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace CarsShop.Db
{
    public class AppDbRequestInfo : DbContext
    {
        public AppDbRequestInfo(DbContextOptions<AppDbRequestInfo> options) : base(options) { }

        // Single table for both Cars and Trucks requests
        //public DbSet<RequestInfo> RequestInfos { get; set; }
        public DbSet<RequestInfo> RequestInfos { get; set; }  // ✅ Add this
        public DbSet<CarInfoRequest> CarRequestInfos { get; set; }  // existing
        public DbSet<TruckRequestInfo> TruckRequestInfos { get; set; } // ✅ add this
    }

    // Use a simple class inside the same file — no separate Models folder needed
    public class RequestInfo
    {
        public int Id { get; set; }
        public int ItemId { get; set; }        // CarId or TruckId
        public string ItemType { get; set; } = string.Empty; // "Car" or "Truck"
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

