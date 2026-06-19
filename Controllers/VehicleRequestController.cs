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
        private readonly IRequestSubmitterService _service;

        public VehicleRequestController(
            IRequestSubmitterService service
        )
        {
            _service = service;
        }

        /*

        [HttpPost]
        public async Task<IActionResult> Create(
            VehicleRequestCreateDto dto
        )
        {
            var userClaim = this.User.Claims.First(x => x.Type == AuthService.ClaimIdKey);
            //int userId = int.Parse(userClaim.Value);

            if (userClaim == null)
                return Unauthorized("User claim not found");

            if (!int.TryParse(userClaim.Value, out int userId))
                return Unauthorized("Invalid user id in token");

            var result = await _service.AddNew(
                dto,
               userId
            );

            return Ok(result);
        }
        */

        [HttpPost]
        public async Task<IActionResult> Create(VehicleRequestCreateDto dto)
        {
            var userClaim = this.User.Claims
                .FirstOrDefault(x => x.Type == AuthService.ClaimIdKey);

            if (userClaim == null)
                return Unauthorized("User claim not found");

            if (!int.TryParse(userClaim.Value, out int userId))
                return Unauthorized("Invalid user id in token");

            Console.WriteLine($"Token UserId: {userId}");

            var result = await _service.AddNew(dto, userId);

            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //var result = await _service.GetListAsync(null);
            return Ok();
        }
    }
}