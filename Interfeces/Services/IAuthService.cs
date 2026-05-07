using CarsShop.Dto.RequestsDto.Login;
using CarsShop.Dto.Responses.Auth;

namespace CarsShop.Services.Auth
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterDto request);
        Task<AuthResponse> LoginAsync(LoginDto request);
    }
}