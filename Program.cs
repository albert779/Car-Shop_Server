using CarsShop.Configurations.Extensions;
using CarsShop.Database;
using CarsShop.Interfeces.Services;
using CarsShop.Middlewares;
using CarsShop.Services.Configuration;
using CarsShop.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;


// ================= Serilog =================

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console(new JsonFormatter())
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);



builder.Host.UseSerilog();

// ================= Connection String =================

var conn = builder.Configuration
    .GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(conn))
{
    throw new InvalidOperationException(
        "DefaultConnection is missing from configuration.");
}

Console.WriteLine("Database connection string configured.");

// ================= DbContext =================

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(conn));

// ================= JWT Configuration =================

var jwtSection = builder.Configuration.GetSection("JWTInfo");
var jwtInfo = jwtSection.Get<JWTInfo>();

builder.Services.AddOptions<JWTInfo>()
    .Bind(jwtSection)
    .ValidateDataAnnotations()
    .ValidateOnStart();





// ================= Build App =================

builder.Services.AddServices(builder.Configuration);

// Register My Requests service
builder.Services.AddScoped<IMyRequestsService, MyRequestsService>();

var app = builder.Build();


// ================= Exception Handling =================

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.ContentType = "application/json";

        var errorFeature = context.Features.Get<
            Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();

        if (errorFeature?.Error is DbUpdateConcurrencyException)
        {
            context.Response.StatusCode =
                StatusCodes.Status409Conflict;

            await context.Response.WriteAsync(
                "{ \"error\": \"The record was changed by another process.\" }");
        }
        else
        {
            context.Response.StatusCode =
                StatusCodes.Status500InternalServerError;

            await context.Response.WriteAsync(
                "{ \"error\": \"Unexpected error occurred.\" }");
        }
    });
});

// ================= Development =================

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// ================= Middleware Pipeline =================

app.UseCors("AllowAngular");

app.UseAuthentication();

app.UseMiddleware<LoggingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
