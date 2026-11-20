using CarsShop.Interfeces.Services;
using CarsShop.Middlewares;
using CarsShop.Services.Auth;
using CarsShop.Services.Configuration;
using CarsShop.Services.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CarsShop.Configurations.Extensions
{
    public static class ConfigurationServicesAdd
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            var emailSettingsSection = configuration.GetSection("EmailSettings");
            services.AddOptions<EmailSettingsConfig>()
                .Bind(emailSettingsSection)
                .ValidateDataAnnotations()
                .ValidateOnStart();



            services.AddControllers();
            services.AddOpenApi();
            services.AddCarsShopServices();

            services.AddScoped<IUpdateRequestService, UpdateRequestService>();
            services.AddScoped<ISendRequestService, SendRequestService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<LoggingMiddleware>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IVehicleRequestService, VehicleRequestService>();
            services.AddScoped<ITableOfRequests, RequestService>();



            var jwtSection = configuration.GetSection("JWTInfo");
            var jwtInfo = jwtSection.Get<JWTInfo>();



            // ================= CORS =================

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular", policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });



            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwtInfo.Issuer,
                        ValidAudience = jwtInfo.Audience,

                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtInfo.Key)),

                        ClockSkew = TimeSpan.Zero
                    };
                });

           services.AddAuthorization();

            return services;

        }
    }
}
