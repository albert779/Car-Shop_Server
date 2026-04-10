using CarsShop.Configuration;
using CarsShop.Configurations;
using CarsShop.Db;
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
using Serilog.Formatting.Json;
using System.Text;

// ================= Serilog =================
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console(new JsonFormatter())
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

// ================= Options / Config =================
builder.Services
    .AddOptions<JWTInfo>()
    .Bind(builder.Configuration.GetSection("JWTInfo"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services
    .AddOptions<EmailSettingsConfig>()
    .Bind(builder.Configuration.GetSection("EmailSettings"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

var jwtInfo = builder.Configuration.GetSection("JWTInfo").Get<JWTInfo>();

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

// ================= DbContexts =================
builder.Services.AddDbContextApp(builder.Configuration);

builder.Services.AddDbContext<AppDbRequestInfo>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("CarInfoRequestsConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)
    )
);

// ================= Scoped services =================
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<ITruckService, TruckService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<LoggingMiddleware>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<IRequestInfoService, RequestInfoService>();
builder.Services.AddScoped<ITruckRequestService, TruckRequestService>();

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
            ValidIssuer = jwtInfo?.Issuer,
            ValidAudience = jwtInfo?.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtInfo?.Key ?? "")),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

// ================= Build App =================
var app = builder.Build();

// ================= Middleware =================
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.ContentType = "application/json";
        var errorFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();

        if (errorFeature?.Error is DbUpdateConcurrencyException)
        {
            context.Response.StatusCode = StatusCodes.Status409Conflict;
            await context.Response.WriteAsync("{ \"error\": \"The record was changed by another process.\" }");
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync("{ \"error\": \"Unexpected error occurred.\" }");
        }
    });
});

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAngular");
app.UseAuthentication();
app.UseMiddleware<LoggingMiddleware>();
app.UseAuthorization();
app.MapControllers();

app.Run();