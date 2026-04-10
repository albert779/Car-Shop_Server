using Azure;
using CarsShop.RequestsDto.Login;
using CarsShop.Responses.API;
using CarsShop.Responses.Auth;
using CarsShop.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IDGCoreWebAPI.Controllers
{
    [AllowAnonymous]
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
        /*
        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterDto request)
        {
            if (request == null)
                return BadRequest("Invalid data");

            var result = await _authService.RegisterAsync(request);

             if (!result)
                return BadRequest("Email already exists");
             
            return CreatedAtAction(nameof(Register), nameof(AuthController), request);
            //return Conflict(new { message = "Email already exists" });
            //return Ok();
        }
        */
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            if (request == null)
                return BadRequest("Invalid data");

            var result = await _authService.RegisterAsync(request);

            if (!result)
                return BadRequest("Email already exists");

            return Ok("User registered successfully");
        }

        // LOGIN
        /*
        [HttpPost("login")]
        public async Task<ActionResult<APIResponse>> Login([FromBody] LoginDto request)
        {
            if (request == null)
                return BadRequest(APIResponseWithError.Create("Invalid data"));

            var result = await _authService.LoginAsync(request);

            if (!result.Success)
                return BadRequest(APIResponseWithError.Create(result.Message));

            //var response = APIResponseWithData<string>.Create(result.Token);
            var response = APIResponseWithData<object>.Create(new
            {
                token = result.Token,
                firstName = result.FirstName,
                lastName = result.LastName,
                email = result.Email,
                phone=result.phone
            });

            return Ok(response);
        }
        */

        [HttpPost("login")]
        public async Task<ActionResult<APIResponse>> Login([FromBody] LoginDto request)
        {
            if (request == null)
                return BadRequest(APIResponseWithError.Create("Invalid data"));

            var result = await _authService.LoginAsync(request);

            if (!result.Success)
                return BadRequest(APIResponseWithError.Create(result.Message));

            var response = APIResponseWithData<AuthResponse>.Create(new AuthResponse
            {
                Token = result.Token,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Email = result.Email,
                Phone = result.Phone
            });

            return Ok(response);
        }
    }
}