using CarsShop.Dto.RequestsDto.Login;
using CarsShop.Dto.Responses.API;
using CarsShop.Dto.Responses.Auth;
using CarsShop.Interfeces.Services;
using CarsShop.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            if (request == null)
                return BadRequest("Invalid data");

            var result = await _authService.RegisterAsync(request);

            if (!result)
                return BadRequest("Email already exists");

            //return Ok("User registered successfully");
            return Ok(APIResponse.CreateOKWithData("User registered successfully"));
        }



        [HttpPost("login")]
        public async Task<ActionResult<APIResponse>> Login([FromBody] LoginDto request)
        {
            APIResponse response = null;

            if (request == null)
            {
                response = APIResponse.CreateBadWithMessage<string>("Invalid data");
                return BadRequest(response);
            }

            var authResponse = await _authService.LoginAsync(request);
            if (authResponse.Success == false)
            {
                response = APIResponse.CreateBadWithMessage<string>(authResponse.Message);
                return BadRequest(response);
            }

            return APIResponse.CreateOKWithData(authResponse);
            /*
            return Ok(APIResponse.CreateOKWithData(new
            {
                token = authResponse.Token,
                role = authResponse.RoleId == 1 ? "Manager" : "User",
                //userId = authResponse.UserId,
                email = authResponse.Email,
                firstName = authResponse.FirstName,
                lastName = authResponse.LastName
            }));
            */
        }
    }
}