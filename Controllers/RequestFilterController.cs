//using CarsShop.DTOs;
using CarsShop.Db.Models;
using CarsShop.Interfeces.Services;
using CarsShop.Services;
using Microsoft.AspNetCore.Mvc;
using CarsShop.Dto.RequestsDto;
using CarsShop.Dto.RequestsDto.RequestFilterDto;
//using CarsShop.Services.Interfaces;

namespace CarsShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequestFilterController : AuthorizedController
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
            var userId = GetUserId();
            //var result = await _requestService.GetRequestsAsync(userId, filter);
            var serviceFilter =
            RequestFilterDto.ConvertToServiceDto(filter);

            var result = await _requestService.GetRequestsAsync(
                GetUserId(),
                serviceFilter);
            return Ok(result);
        }
    }
}
