using CarsShop.RequestsDto.Login;
using CarsShop.Responses.Auth;
using CarsShop.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace IDGCoreWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        // REGISTER
        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterDto request)
        {
            if (request == null)
                return BadRequest("Invalid data");

            var result = await _authService.RegisterAsync(request);

            if (!result)
                return BadRequest("Email already exists");

            return CreatedAtAction(nameof(Register), nameof(AuthController), request);
        }

        // LOGIN
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginDto request)
        {
            if (request == null)
                return BadRequest("Invalid data");

            var result = await _authService.LoginAsync(request);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}