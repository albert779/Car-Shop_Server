using CarsShop.Db;
using Microsoft.EntityFrameworkCore;

namespace CarsShop.Configurations
{
    public static class DBConfigurations
    {

        public static void AddDbContextApp(this IServiceCollection service, IConfigurationManager configurationManager)
        {
            service.AddDbContext<AppDbCar>(options =>
    options.UseSqlServer(configurationManager.GetConnectionString("CarsConnection")));

            service.AddDbContext<AppDbTruck>(options =>
                options.UseSqlServer(configurationManager.GetConnectionString("TrucksConnection")));

            service.AddDbContext<AppDbUser>(options =>
                options.UseSqlServer(configurationManager.GetConnectionString("UsersConnection")));
        }
    }
}
