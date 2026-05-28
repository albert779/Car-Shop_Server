using CarsShop.Dto.RequestsDto.Vehicle.Item;
using CarsShop.Interfeces.Db;
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
        public async Task<IActionResult> Create(
            VehicleRequestCreateDto dto
        )
        {
            var userClaim = this.User.Claims.First(x => x.Type == AuthService.ClaimIdKey);
            int userId = int.Parse(userClaim.Value);

            var result = await _service.CreateAsync(
                dto,
               userId
            );

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetListAsync(null);

            return Ok(result);
        }
    }
}