/*
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

*/

using CarsShop.Db.Models;
using CarsShop.Interfeces.Db;
using CarsShop.RequestsDto.CarsShop;
using CarsShop.Responses.API;
using CarsShop.Responses.CarsShop;
using CarsShop.Responses.TrucksShop;
using CarsShop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CarsShop.RequestsDto.TrucksShop;


//using CarsShop.Interfeces.Db;
//using CarsShop.RequestsDto.TrucksShop;
//using CarsShop.RequestsDto.CarsShop;
//using CarsShop.Responses.TrucksShop;
//using CarsShop.Services;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Authorization;
namespace CarsShop.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TrucksController : ControllerBase
    {
        private readonly ITruckService _truckService;

        public TrucksController(ITruckService truckService)
        {
            _truckService = truckService;
        }

        // =========================
        // GET ALL
        // GET: api/trucks
        // =========================
        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetAll()
        {
            var trucks = await _truckService.GetListAsync();

            if (!trucks.Any())
                return NotFound(APIResponseWithError.Create("No trucks found"));

            // Use IEnumerable<GetCarstResponse>, not List<Truck>
            return Ok(
    APIResponseWithData<IEnumerable<CarsShop.Responses.TrucksShop.GetCarstResponse>>.Create(trucks));
        }
        

        // =========================
        // GET BY ID
        // GET: api/trucks/5
        // =========================
        [HttpGet("{id}")]
        public async Task<ActionResult<APIResponse>> GetById(int id)
        {
            var truck = await _truckService.GetByIdAsync(id);

            if (truck == null)
            {
                return NotFound(
                    APIResponseWithError.Create($"Truck with id {id} not found")
                );
            }

            return Ok(
                APIResponseWithData<Truck>.Create(truck)
            );
        }

        // =========================
        // CREATE
        // POST: api/trucks
        // =========================
        [HttpPost]
        public async Task<ActionResult<APIResponse>> Create([FromBody] TrucksCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(
                    APIResponseWithError.Create("Invalid truck data")
                );
            }

            var createdTruck = await _truckService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdTruck.Id },
                APIResponseWithData<Truck>.Create(createdTruck)
            );
        }

        // =========================
        // UPDATE
        // PUT: api/trucks/5
        // =========================
        [HttpPut("{id}")]
        public async Task<ActionResult<APIResponse>> Update(
            int id,
            [FromBody] TrucksUpdateDto dto)
        {
            var updated = await _truckService.UpdateAsync(id, dto);

            if (!updated)
            {
                return NotFound(
                    APIResponseWithError.Create($"Truck with id {id} not found")
                );
            }

            return Ok(
                APIResponseWithData<int>.Create(id)
            );
        }

        // =========================
        // DELETE
        // DELETE: api/trucks/5
        // =========================
        [HttpDelete("{id}")]
        public async Task<ActionResult<APIResponse>> Delete(int id)
        {
            var deleted = await _truckService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound(
                    APIResponseWithError.Create($"Truck with id {id} not found")
                );
            }

            return Ok(
                APIResponseWithData<int>.Create(id)
            );
        }
    }
}