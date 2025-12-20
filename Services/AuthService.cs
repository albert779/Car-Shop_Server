using CarsShop.Db;
using CarsShop.Db.Models;
using CarsShop.RequestsDto.Login;
using CarsShop.Responses.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly string _jwtKey = "THIS_IS_SECRET_KEY_CHANGE_ME";
        private readonly AppDbUser _db;

        private static readonly string ErrorEmailOrPasswordWrong = "Invalid email or password";


        public AuthService(AppDbUser db)
        {
            this._db = db;
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
                return new AuthResponse(false, string.Empty, AuthService.ErrorEmailOrPasswordWrong);

            var result = _hasher.VerifyHashedPassword(user, user.Password, request.Password);

            if (result == PasswordVerificationResult.Failed)
                return new AuthResponse(false, string.Empty, AuthService.ErrorEmailOrPasswordWrong);

            string token = GenerateJwt(user);

            return new AuthResponse(true, token);
        }

        private string GenerateJwt(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("id", user.ID.ToString())
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}