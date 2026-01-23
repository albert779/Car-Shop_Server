using CarsShop.Configuration;
using CarsShop.Db;
using CarsShop.Db.Models;
using CarsShop.RequestsDto.Login;
using CarsShop.Responses.API;
using CarsShop.Responses.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarsShop.Services.Auth
{
    public class AuthService : IAuthService
    {
        //private readonly List<User> _users = new();
        private readonly PasswordHasher<User> _hasher;
        private readonly AppDbUser _db;
        private readonly JWTInfo _jwtOptions;
        private static readonly string ErrorEmailOrPasswordWrong = "Invalid email or password";


        public AuthService(AppDbUser db, IOptions<JWTInfo> jwtOptions)
        {
            this._db = db;
            this._jwtOptions = jwtOptions.Value;
            this._hasher = new();
        }

        public async Task<bool> RegisterAsync(RegisterDto request)
        {
            if (_db.Users.Any(u => u.Email == request.Email))
                return false;

            var dbUser = request.ConvertToDbModel();
            dbUser.Password = _hasher.HashPassword(dbUser, request.Password);

            _db.Add(dbUser);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }

            return true;
        }

        public async Task<AuthResponse> LoginAsync(LoginDto request)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
                return AuthResponse.GetResponseWithError(AuthService.ErrorEmailOrPasswordWrong);

            var result = _hasher.VerifyHashedPassword(user, user.Password, request.Password);

            if (result == PasswordVerificationResult.Failed)
                return AuthResponse.GetResponseWithError(AuthService.ErrorEmailOrPasswordWrong);

            string token = GenerateJwt(user);

            var tokenResponse = AuthResponse.GetResponseWithToken(token);
            return tokenResponse;
        }

        private string GenerateJwt(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("id", user.Id.ToString())
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_jwtOptions.ExpiresInMinutes),
                signingCredentials: creds,
                audience:_jwtOptions.Audience,
                issuer : _jwtOptions.Issuer
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

