using CarsShop.Dto.RequestsDto.RequestFilterDto;
using CarsShop.Dto.Responses.API;
using CarsShop.Interfeces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarsShop.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class VehicleRequestController : AuthorizedController
    {
        private readonly IVehicleRequestService _service;

        public VehicleRequestController(IVehicleRequestService service
        )
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VehicleRequestCreateDto dto)
        {

            var userId = GetUserId();
            var item = VehicleRequestCreateDto.ConvertServiceDto(dto);
            await _service.AddNew(item, userId);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetRequests([FromQuery] RequestFilterDto filter)
        {
            var userId = GetUserId();
            if(filter == null)
            {
                var responose = APIResponse.CreateBadWithMessage("Filters cannot be null");
                return responose;
            }

            var item = RequestFilterDto.ConvertToServiceDto(filter);
            var result = await _service.GetRequestsAsync(userId, item);
            var response = APIResponse.CreateOKWithData(result);
            return response;
        }
    }
}