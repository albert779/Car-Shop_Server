using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace CarsShop.Interfeces.Services
{
    public interface IJwtService
    {
        public string GenerateJwt(IEnumerable<Claim> claims, SigningCredentials signingCredentials);
    }
}
