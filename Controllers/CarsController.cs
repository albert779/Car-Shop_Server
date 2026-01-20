using CarsShop.Interfeces.Db;
using CarsShop.RequestsDto.CarsShop;
using CarsShop.Responses.CarsShop;
using CarsShop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IDGCoreWebAPI.Controllers
{

    namespace CarsShop.Controllers
    {
        [Authorize]
        [ApiController]
        [Route("api/[controller]")]
        public class CarsController : ControllerBase
        {
            private readonly ILogger<CarsController>? _logger;
            private readonly ICarService _carService;

            public CarsController(ILogger<CarsController> logger, ICarService car_carService)
            {
                _logger = logger;
                ArgumentNullException.ThrowIfNull(car_carService);
                this._carService = car_carService;
            }
            //[AllowAnonymous]
            //[Authorize]
            [HttpGet]
            public ActionResult<IEnumerable<GetCarstResponse>> GeAll()
            {
                var result = _carService.GetList();

                if (result == null)
                    return NotFound();

                return Ok(result);
            }


            [HttpPost]
            public async Task<ActionResult<GetCarstResponse>> Create([FromBody] CarsCreateDto request)
            {
                if (request == null)
                    return BadRequest();

                //var response = _carService.AddAsync(request);
                //return Ok(response);

                // return Task.FromResult<ActionResult<GetCarstResponse>>(Ok(response));

                try
                {
                    var response = await _carService.AddAsync(request); // <-- await here
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    // log ex somewhere
                    return StatusCode(500, ex.ToString());
                }
            }

            [HttpPut("{id}")]
            public async Task<ActionResult<GetCarstResponse>> Update([FromRoute] int id, [FromBody] CarsUpdateDto request)
            {
                if (id < 1)
                {
                    return BadRequest("ID in URL does not match ID");
                }

                GetCarstResponse response = _carService.UpdateAsync(id, request);
                if (request == null)
                {
                    return NotFound($"the item with id {id} not found");
                }

                return Ok(response);
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete([FromRoute] int id)
            {

                bool isDeleted = _carService.DeleteAsync(id);

                if (isDeleted == false)
                {
                    return NotFound($"the item with id {id} not found");
                }

                return NoContent();
            }
        }
    }
}

