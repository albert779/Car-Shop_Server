using CarsShop.Dto.RequestsDto.Vehicle.Item;
using CarsShop.Interfeces.Db;
using CarsShop.Interfeces.Services;
using CarsShop.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarsShop.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class VehicleRequestController : AuthorizedController
    {
        private readonly IVehicleRequestService _service;

        public VehicleRequestController(
            IVehicleRequestService service
        )
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VehicleRequestCreateDto dto)
        {
            var userId = GetUserId();
            await _service.AddNew(dto, userId);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetRequests(
    [FromQuery] RequestFilterDto filter)
        {
            var userId = GetUserId();

            var result = await _service.GetRequestsAsync(
                userId,
                filter);

            return Ok(result);
        }
    }
}