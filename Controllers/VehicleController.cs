using CarsShop.Interfeces.Db;
using CarsShop.RequestsDto.Vehicle.Item;
using CarsShop.Responses.API;
using CarsShop.Responses.CarsShop;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CarsShop.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetAll([FromQuery] string? type)
        {
            var vehicles = await _vehicleService.GetListAsync(type);

            if (!vehicles.Any())
                return NotFound(APIResponseWithError.Create("No vehicles found"));

            return Ok(APIResponseWithData<IEnumerable<GetVehicleResponse>>.Create(vehicles));
        }
        // =========================
        // GET BY ID
        // =========================
        [HttpGet("{id}")]
        public async Task<ActionResult<APIResponse>> GetById(int id)
        {
            var vehicle = await _vehicleService.GetByIdAsync(id);

            if (vehicle == null)
                return NotFound(APIResponseWithError.Create($"Vehicle with id {id} not found"));

            return Ok(
                APIResponseWithData<GetVehicleResponse>.Create(vehicle)
            );
        }

        // =========================
        // CREATE
        // =========================
        [HttpPost]
        public async Task<ActionResult<APIResponse>> Create([FromBody] VehicleItemCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(APIResponseWithError.Create("Invalid vehicle data"));

            var created = await _vehicleService.AddAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                APIResponseWithData<GetVehicleResponse>.Create(created)
            );
        }

        // =========================
        // UPDATE
        // =========================
        [HttpPut("{id}")]
        public async Task<ActionResult<APIResponse>> Update(int id, [FromBody] VehicleItemUpdateDto request)
        {
            var updated = await _vehicleService.UpdateAsync(id, request);

            if (updated == null)
                return NotFound(APIResponseWithError.Create($"Vehicle with id {id} not found"));

            return Ok(APIResponseWithData<int>.Create(id));
        }

        // =========================
        // DELETE
        // =========================
        [HttpDelete("{id}")]
        public async Task<ActionResult<APIResponse>> Delete(int id)
        {
            var deleted = await _vehicleService.DeleteAsync(id);

            if (!deleted)
                return NotFound(APIResponseWithError.Create($"Vehicle with id {id} not found"));

            return Ok(APIResponseWithData<int>.Create(id));
        }
    }
}