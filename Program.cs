

using CarsShop.Configuration;
using CarsShop.Configurations;
using CarsShop.Interfeces.Db;
using CarsShop.Interfeces.Services;
using CarsShop.Middlewares;
using CarsShop.Services;
using CarsShop.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;
using System.Text;


// 🔥 Configure Serilog FIRST
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) // reduce framework noise
    .Enrich.FromLogContext()
    .WriteTo.Console(new JsonFormatter())
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();


// ================= JWT =================
builder.Services
    .AddOptions<JWTInfo>()
    .Bind(builder.Configuration.GetSection("JWTInfo"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

var jwtInfo = builder.Configuration
    .GetSection("JWTInfo")
    .Get<JWTInfo>();


// ================= Services =================
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddDbContextApp(builder.Configuration);

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<ITruckService, TruckService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<LoggingMiddleware>();


// ================= Authentication =================
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtInfo!.Issuer,
            ValidAudience = jwtInfo.Audience,
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtInfo.Key)
                ),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();


// ================= Exception Handling =================
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.ContentType = "application/json";

        var errorFeature = context.Features
            .Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();

        if (errorFeature?.Error is DbUpdateConcurrencyException)
        {
            context.Response.StatusCode = StatusCodes.Status409Conflict;
            await context.Response.WriteAsync(
                "{ \"error\": \"The record was changed by another process.\" }"
            );
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync(
                "{ \"error\": \"Unexpected error occurred.\" }"
            );
        }
    });
});


// ================= Pipeline Order =================

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAngular");

app.UseAuthentication();

// 🔥 Your logging middleware (after auth so user claims exist)
app.UseMiddleware<LoggingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();