using CarsShop.Dto.RequestsDto.Vehicle.Item;
using CarsShop.Interfeces.Db;
using CarsShop.Interfeces.Services;
using CarsShop.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarsShop.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleRequestController : ControllerBase
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
            var userClaim = this.User.Claims
                .FirstOrDefault(x => x.Type == AuthService.ClaimIdKey);

            if (userClaim == null)
                return Unauthorized("User claim not found");

            if (!int.TryParse(userClaim.Value, out int userId))
                return Unauthorized("Invalid user id in token");

            await _service.AddNew(dto, userId);

            return Ok();
        }
    }
}