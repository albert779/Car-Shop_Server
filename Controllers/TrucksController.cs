using CarsShop.Interfeces.Db;
using CarsShop.RequestsDto.TrucksShop;
using CarsShop.RequestsDto.CarsShop;
using CarsShop.Responses.TrucksShop;
using CarsShop.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace IDGCoreWebAPI.Controllers
{

    namespace CarsShop.Controllers
    {
        [Authorize]
        [ApiController]
        [Route("api/[controller]")]
        public class TrucksController : ControllerBase
        {
            private readonly ILogger<TrucksController>? _logger;
            private readonly ITruckService _truckService;

            public TrucksController(ILogger<TrucksController> logger, ITruckService truck_Service)
            {
                _logger = logger;
                ArgumentNullException.ThrowIfNull(truck_Service);
                this._truckService = truck_Service;
            }



            [AllowAnonymous]
            [HttpGet]
            public ActionResult<IEnumerable<GetCarstResponse>> GeAll()
            {

                var result = _truckService.GetList();

                if (result == null)
                    return NotFound();

                return Ok(result);
            }


            [HttpPost]
            public async Task<ActionResult<GetCarstResponse>> Create([FromBody] TrucksCreateDto request)
            {
                if (request == null)
                    return BadRequest();

                GetCarstResponse response = _truckService.AddAsync(request);

                return Ok(response);
            }

            [HttpPut("{id}")]
            public async Task<ActionResult<GetCarstResponse>> Update([FromRoute] int id, [FromBody] TrucksUpdateDto request)
            {
                if (id < 1)
                {
                    return BadRequest("ID in URL does not match ID");
                }

                GetCarstResponse response = _truckService.UpdateAsync(id, request);
                if (request == null)
                {
                    return NotFound($"the item with id {id} not found");
                }

                return Ok(response);
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete([FromRoute] int id)
            {

                bool isDeleted = _truckService.DeleteAsync(id);

                if (isDeleted == false)
                {
                    return NotFound($"the item with id {id} not found");
                }

                return NoContent();
            }
        }
    }
}

