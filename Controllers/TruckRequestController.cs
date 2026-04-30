using CarsShop.Db.Models;
using CarsShop.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarsShop.Api.Controllers
{
    [ApiController]
    [Route("api/trucks/request")]
    public class TruckRequestController : ControllerBase
    {
        private readonly ITruckRequestService _service;

        public TruckRequestController(ITruckRequestService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> PostRequest([FromBody] VehicleRequest request)
        {
            if (request == null)
                return BadRequest("Request is null");

            var result = await _service.AddRequestAsync(request);
            return Ok(result);
        }
    }
}