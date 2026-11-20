using CarsShop.Interfeces.Services;
using CarsShop.Services.Implementations;

namespace CarsShop.Configurations.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCarsShopServices(
            this IServiceCollection services)
        {
            services.AddScoped<
                IVehicleRequestService,
                VehicleRequestService>();

            services.AddScoped<
                IVehicleService,
                VehicleService>();
            /*
            services.AddScoped<
                IUserService,
                UserService>();
            */

            services.AddScoped<
                IEmailService,
                EmailService>();

            services.AddScoped<
                IJwtService,
                JwtService>();

            return services;
        }
    }
}