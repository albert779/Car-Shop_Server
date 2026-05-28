using CarsShop.Dto.RequestsDto.Vehicle.Item;
using CarsShop.Dto.Responses.API;
using CarsShop.Dto.Responses.VehicleShop;
using CarsShop.Interfeces.Db;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CarsShop.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleRequestService _vehicleService;

        public VehicleController(IVehicleRequestService vehicleService)
        {
            _vehicleService = vehicleService;
        }


        /*
        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetAll([FromQuery] string? type)
        {
            var vehicles = await _vehicleService.GetListAsync(type);

            if (!vehicles.Any())
            {
                var error = APIResponse.CreateBadWithMessage<string>("No vehicles found");
                return NotFound(error);
            }

            var response = APIResponse.CreateOKWithData(vehicles);
            return Ok(response);
        }
        // =========================
        // GET BY ID
        // =========================
        [HttpGet("{id}")]
        public async Task<ActionResult<APIResponse>> GetById(int id)
        {
            var vehicle = await _vehicleService.GetByIdAsync(id);

            if (vehicle == null)
            {
                var error = APIResponse.CreateBadWithMessage("Vehicle with id {id} not found");
                return NotFound(error);

            }
            return APIResponse.CreateOKWithData<GetVehicleResponse>(vehicle);
        }
        */

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetAll([FromQuery] string? type)
        {
            var vehicles = await _vehicleService.GetListAsync(type);

            // ✅ Return empty list instead of 404
            if (vehicles == null)
            {
                vehicles = new List<GetVehicleResponse>();
            }

            var response = APIResponse.CreateOKWithData(vehicles);

            return Ok(response);
        }

        // =========================
        // GET BY ID
        // =========================

        [HttpGet("{id}")]
        public async Task<ActionResult<APIResponse>> GetById(int id)
        {
            var vehicle = await _vehicleService.GetByIdAsync(id);

            if (vehicle == null)
            {
                var error =
                    APIResponse.CreateBadWithMessage(
                        $"Vehicle with id {id} not found"
                    );

                return NotFound(error);
            }

            return Ok(
                APIResponse.CreateOKWithData<GetVehicleResponse>(vehicle)
            );
        }

        // =========================
        // CREATE
        // =========================
        [HttpPost]
        public async Task<ActionResult<APIResult>> Create([FromBody] VehicleItemCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                var error = APIResponse.CreateBadWithMessage("Invalid vehicle data");
                return BadRequest(error);
            }

            var created = await _vehicleService.AddAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                APIResponse.CreateOKWithData(created)
            );
        }

        // =========================
        // UPDATE
        // =========================
        [HttpPut("{id}")]
        public async Task<ActionResult<APIResult>> Update(int id, [FromBody] VehicleItemUpdateDto request)
        {
            var updated = await _vehicleService.UpdateAsync(id, request);

            if (updated == null)
            {
                var error = APIResponse.CreateBadWithMessage<string>("Vehicle with id {id} not found");
                return NotFound(error);
            }
            var response = APIResponse.CreateOKWithData<int>(id);

            return Ok(response);
        }

        // =========================
        // DELETE
        // =========================
        [HttpDelete("{id}")]
        public async Task<ActionResult<APIResult>> Delete(int id)
        {
            var deleted = await _vehicleService.DeleteAsync(id);

            if (!deleted)
            {
                var error = APIResponse.CreateBadWithMessage<string>($"Vehicle with id ${id} not found");
                return NotFound(error);
            }

            var response = APIResponse.CreateOK();
            return Ok(response);
        }


        [HttpGet("search")]
        public async Task<ActionResult<APIResponse>> Search(
                   [FromQuery] string? text)
        {
            var vehicles =
                await _vehicleService.SearchAsync(text);

            var response =
                APIResponse.CreateOKWithData(vehicles);

            return Ok(response);
        }
    }
}