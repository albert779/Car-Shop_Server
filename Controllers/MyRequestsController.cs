using CarsShop.Dto.Responses.API;
using CarsShop.Interfeces.Services;
using CarsShop.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarsShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DashboardController : AuthorizedController
    {
        private readonly IVehicleRequestService _vehicleRequestService;
        private readonly IMyRequestsService _myRequestsService;

        public DashboardController(
            IVehicleRequestService vehicleRequestService,
            IMyRequestsService myRequestsService)
        {
            _vehicleRequestService = vehicleRequestService
                ?? throw new ArgumentNullException(nameof(vehicleRequestService));

            _myRequestsService = myRequestsService
                ?? throw new ArgumentNullException(nameof(myRequestsService));
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var userId = GetUserId();

            if (userId == -1)
                return Unauthorized();

            var result =
                await _vehicleRequestService.GetDashboardAsync(userId);

            return Ok(APIResponse.CreateOKWithData(result));
        }

        [HttpGet("my-requests")]
        public async Task<IActionResult> GetMyRequests(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 5)
        {
            var userId = GetUserId();

            if (userId == -1)
                return Unauthorized();

            page = Math.Max(1, page);
            pageSize = Math.Clamp(pageSize, 1, 10);

            var result = await _myRequestsService.GetMyRequestsAsync(
                userId, page, pageSize);

            return Ok(APIResponse.CreateOKWithData(result));
        }
    }
}
