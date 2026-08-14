
using CarsShop.Interfeces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarsShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TableOfRequestsController : ControllerBase
    {

        private readonly ITableOfRequests _service;


        public TableOfRequestsController(ITableOfRequests service)
        {
            _service = service;
        }



        [HttpGet]
        public async Task<IActionResult> GetRequests([FromQuery] string? search)
        {
            var result = await _service.GetRequests(search);

            return Ok(result);
        }

    }
}


/*
using CarsShop.Interfeces.Services;
using CarsShop.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarsShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TableOfRequestsController : ControllerBase
    {

        private readonly ITableOfRequests _service;


        public TableOfRequestsController(ITableOfRequests service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetRequests()
        {
            var userIdClaim = User.FindFirst(AuthService.ClaimIdKey);

            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var userId = int.Parse(userIdClaim.Value);

            var result = await _service.GetRequests(userId);

            return Ok(result);
        }

    }
}
*/