//using CarsShop.DTOs;
using CarsShop.Db.Models;
using CarsShop.Interfeces.Services;
using CarsShop.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarsShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequestFilterController : ControllerBase
    {
        private readonly IVehicleRequestService _requestService;

        public RequestFilterController(IVehicleRequestService requestService)
        {
            _requestService = requestService;
        }


        [HttpGet]
        public async Task<IActionResult> GetRequests(
            [FromQuery] RequestFilterDto filter)
        {
            var userId = int.Parse(
                User.FindFirst("id")!.Value
            );

            var result = await _requestService
                .GetRequestsAsync(userId, filter);

            return Ok(result);
        }
    }
}
