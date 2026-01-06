using CarsShop.Configurations;
using CarsShop.Interfeces.Db;
using CarsShop.Services;
using CarsShop.Services.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CarsShop.Configuration;


var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOptions<JWTInfo>()
    .Bind(builder.Configuration.GetSection("JWTInfo"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

var jwtInfo = builder.Configuration
    .GetSection("JWTInfo")
    .Get<JWTInfo>();

//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // Angular dev server
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


builder.Services.AddDbContextApp(builder.Configuration);

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<ITruckService, TruckService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtInfo.Key)
                ),
            ClockSkew = TimeSpan.Zero

        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

//app.UseCors("AllowAngular"); 


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

//app.UseAuthorization();


//app.MapControllers();


// ?? Global error handling middleware
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.ContentType = "application/json";

        var errorFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
        if (errorFeature?.Error is DbUpdateConcurrencyException)
        {
            context.Response.StatusCode = StatusCodes.Status409Conflict; // 409 = Conflict
            await context.Response.WriteAsync(
                "{ \"error\": \"The record you attempted to modify or delete does not exist or was changed by another process.\" }"
            );
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync(
                "{ \"error\": \"An unexpected error occurred.\" }"
            );
        }
    });
});

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// 2?? CORS MUST be before auth & controllers
app.UseCors("AllowAngular");

// 3?? Security middleware
app.UseAuthentication();
app.UseAuthorization();

// 4?? Endpoints LAST
app.MapControllers();

app.Run();




// Remove the invalid 'nullbuilder' and fix the closing parentheses and semicolon.
// The correct code for AddDbContext with EnableRetryOnFailure should be:

/*
builder.Services.AddDbContext<AppDb>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,             // how many retries
            maxRetryDelay: TimeSpan.FromSeconds(10), // wait between retries
            errorNumbersToAdd: null
        )
    )
);
*/