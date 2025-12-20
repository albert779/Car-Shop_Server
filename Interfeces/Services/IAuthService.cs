using CarsShop.RequestsDto.Login;
using CarsShop.Responses.Auth;

namespace CarsShop.Services.Auth
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterDto request);
        Task<AuthResponse> LoginAsync(LoginDto request);
    }
}