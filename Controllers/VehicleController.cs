using CarsShop.Dto.RequestsDto.Vehicle.Item;
using CarsShop.Dto.Responses.API;
using CarsShop.Dto.Responses.VehicleShop;
using CarsShop.Interfeces.Services;
using Microsoft.AspNetCore.Mvc;

using ServiceVehicleItemUpdateDto =
    CarsShop.Models.Requests.Vehicle.Item.VehicleItemUpdate;

namespace CarsShop.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : AuthorizedController
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

       

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetAll(
        [FromQuery] string? type)
        {
            var vehicles = await _vehicleService.GetListAsync(type);

            if (vehicles == null)
            {
                return Ok(APIResponse.CreateOKWithData(
                    new List<GetVehicleResponse>()));
            }

            return Ok(APIResponse.CreateOKWithData(vehicles));
        }

        // =========================
        // GET BY ID
        // =========================


        /*
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
        */

        [HttpGet("{id}")]
        public async Task<ActionResult<APIResponse>> GetById(int id)
        {
            var serviceVehicle = await _vehicleService.GetByIdAsync(id);

            if (serviceVehicle == null)
            {
                return NotFound(
                    APIResponse.CreateBadWithMessage(
                        $"Vehicle with id {id} not found"));
            }

            var vehicle = new GetVehicleResponse
            {
                // Map properties here
            };

            return Ok(APIResponse.CreateOKWithData(vehicle));
        }

        // =========================
        // CREATE
        // =========================

        /*
        [HttpPost]
        public async Task<ActionResult<APIResult>> Create([FromBody] VehicleItemCreateDto dto)
        {
            if (!ModelState.IsValid || dto == null)
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
        */

        // =========================
        [HttpPost]
        public async Task<ActionResult<APIResult>> Create(
            [FromBody] VehicleItemCreateDto dto)
        {
            if (!ModelState.IsValid || dto == null)
            {
                var error = APIResponse.CreateBadWithMessage("Invalid vehicle data");
                return BadRequest(error);
            }

            var serviceDto =
                new CarsShop.Models.Requests.Vehicle.Item.VehicleItemCreate
                {
                    Color = dto.Color,
                    Model = dto.Model,
                    Date = dto.Date,
                    Details = dto.Details,
                    Image = dto.Image,
                    Price = dto.Price,
                    VehicleTypeId = dto.VehicleTypeId
                };

            var created = await _vehicleService.AddAsync(serviceDto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                APIResponse.CreateOKWithData(created)
            );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<APIResponse>> Update(
            int id,
            [FromBody] VehicleItemUpdateDto dto)
        {
            if (!ModelState.IsValid || dto == null)
            {
                return BadRequest(
                    APIResponse.CreateBadWithMessage("Invalid vehicle data"));
            }

            var serviceDto = new ServiceVehicleItemUpdateDto
            {
                Color = dto.Color,
                Model = dto.Model,
                Date = dto.Date,
                Details = dto.Details,
                Image = dto.Image,
                Price = dto.Price
            };

            var updated = await _vehicleService.UpdateAsync(id, serviceDto);

            return Ok(APIResponse.CreateOKWithData(updated));
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