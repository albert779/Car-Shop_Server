
using CarsShop.Configuration;
using CarsShop.Db;
using CarsShop.Db.Models;
using CarsShop.Dto.RequestsDto.Login;
using CarsShop.Dto.Responses.Auth;
using CarsShop.Interfeces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace CarsShop.Services.Auth
{
    public class AuthService : IAuthService
    {
        public static string ClaimIdKey = "id";
        private readonly PasswordHasher<User> _hasher;
        //private readonly AppDbUser _db;
        private readonly AppDbContext _db;
        private readonly string _key;
        private readonly IJwtService _jwtService;
        private static readonly string ErrorEmailOrPasswordWrong = "Invalid email or password";




        public AuthService(AppDbContext db, IJwtService jwtService, IOptions<JWTInfo> jwtOptions)
        {
            this._db = db;
            this._key= jwtOptions.Value.Key;
            this._jwtService = jwtService;
            this._hasher = new();
        }

        public async Task<bool> RegisterAsync(RegisterDto request)
        {
            if (_db.Users.Any(u => u.Email == request.Email))
                return false;

            var dbUser = request.ConvertToDbModel();
            dbUser.Password = _hasher.HashPassword(dbUser, request.Password);

            _db.Users.Add(dbUser);
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
            var email = request.Email.ToLower();
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email);

            if (user == null)
                return AuthResponse.GetResponseWithError(ErrorEmailOrPasswordWrong);

            var result = _hasher.VerifyHashedPassword(user, user.Password, request.Password);
            if (result == PasswordVerificationResult.Failed)
                return AuthResponse.GetResponseWithError(ErrorEmailOrPasswordWrong);

            var claims = GetUserClaims(user);
            string token = GenerateJwt(claims);

            return AuthResponse.GetResponseWithToken(
                token,
                user.FirstName,   // changed from user.Name
                user.LastName,
                user.Email,
                user.Phone
                
            );
        }
        private string GenerateJwt(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //IEnumerable<Claim> claims = GetUserClaims(user);
            string token = _jwtService.GenerateJwt(claims, creds);
            return token;
        }

        private IEnumerable<Claim> GetUserClaims(User user)
        {
            return new[]
            {
                new Claim("FirstName", user.FirstName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(AuthService.ClaimIdKey, user.Id.ToString()),
                //new Claim("roleId", user.RoleId.ToString())
                new Claim(ClaimTypes.Role,
user.RoleId == 1 ? "Manager" : "User"
        )
            };
        }
    }
}