using CarsShop.Dto.RequestsDto.Login;
using CarsShop.Dto.Responses.API;
using CarsShop.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CarsShop.Dto.RequestsDto.Login;
using CarsShop.Services.Mappers;
using CarsShop.Models.Requests;

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

        /*
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUiDto request)
        {
            if (request == null)
                return BadRequest("Invalid data");

            var result = await _authService.RegisterAsync(request);

            if (!result)
                return BadRequest("Email already exists");

            //return Ok("User registered successfully");
            return Ok(APIResponse.CreateOKWithData("User registered successfully"));
        }
        */
        // REGISTER
        [HttpPost("register")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterUiDto request)
        {
            if (request == null)
                return BadRequest("Invalid data");

            var blRequest = new RegisterBl
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password,
                BirthDate = request.BirthDate,
                Phone = request.Phone,
                RoleId = request.RoleId
            };


            //var result = await _authService.RegisterAsync(blRequest);


            var result = await _authService.RegisterAsync(blRequest);

            if (!result)
                return BadRequest("Email already exists");

            return Ok(APIResponse.CreateOKWithData("User registered successfully"));
        }


        [HttpPost("login")]
        public async Task<ActionResult<APIResponse>> Login([FromBody] LoginUiDto request)
        {
            APIResponse response = null;

            //if (request == null)
            //{
            //    response = APIResponse.CreateBadWithMessage<string>("Invalid data");
            //    return BadRequest(response);
            //}   

            //var authResponse = await _authService.LoginAsync(request);

            var sertviceModel = LoginUiDto.ConvertToServiceLogin(request);
            var authResponse = await _authService.LoginAsync(sertviceModel);
            
            if (authResponse.Success == false)
            {
                response = APIResponse.CreateBadWithMessage<string>(authResponse.Message);
                return BadRequest(response);
            }

            return APIResponse.CreateOKWithData(authResponse);

        }
    }
}