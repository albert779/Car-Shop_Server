using CarsShop.Configuration;
using CarsShop.Interfeces.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CarsShop.Services
{
    public class JwtService : IJwtService
    {
        private readonly JWTInfo _jwtOptions;

        public JwtService(IOptions<JWTInfo> jwtOptions)
        {
            this._jwtOptions = jwtOptions.Value;
        }

        public string GenerateJwt(IEnumerable<Claim> claims, SigningCredentials signingCredentials )
        {
            var jwtSecurityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_jwtOptions.ExpiresInMinutes),
                signingCredentials: signingCredentials,
                audience: _jwtOptions.Audience,
                issuer: _jwtOptions.Issuer
            );

            string token =  new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return token;
        }

     }
}
