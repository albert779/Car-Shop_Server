using CarsShop.Interfeces.Db;
using CarsShop.RequestsDto.CarsShop;
using CarsShop.Responses.API;
using CarsShop.Responses.CarsShop;
using CarsShop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Diagnostics.Tracing.Parsers.AspNet;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace CarsShop.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly ILogger<CarsController> _logger;

        public CarsController(ICarService carService, ILogger<CarsController> logger)
        {
            _carService = carService;
            this._logger = logger;
        }

        // ============================
        // GET ALL
        // ============================
        [HttpGet]
        public ActionResult<APIResponse> GetAll()
        {
            _logger.LogInformation("Fetching all cars");
            var cars = _carService.GetList();

            if (cars == null || !cars.Any())
            {
                return Ok(APIResponseWithError.Create("No cars found"));
            }

            return Ok(APIResponseWithData<IEnumerable<GetCarstResponse>>.Create(cars));
        }

        // ============================
        // CREATE
        // ============================
        [HttpPost]
        public async Task<ActionResult<APIResponse>> Create([FromBody] CarsCreateDto request)
        {
            if (request == null)
            {
                return BadRequest(APIResponseWithError.Create("Invalid request"));
            }

            try
            {
                var createdCar = await _carService.AddAsync(request);

                return Ok(APIResponseWithData<GetCarstResponse>.Create(createdCar));
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    APIResponseWithError.Create("An unexpected error occurred")
                );
            }
        }

        // ============================
        // UPDATE
        // ============================
        [HttpPut("{id}")]
        public async Task<ActionResult<APIResponse>> Update(
            [FromRoute] int id,
            [FromBody] CarsUpdateDto request)
        {
            if (id <= 0 || request == null)
            {
                return BadRequest(APIResponseWithError.Create("Invalid data"));
            }

            // Remove 'await' since UpdateAsync is not actually async and returns GetCarstResponse synchronously
            var updatedCar = _carService.UpdateAsync(id, request);

            if (updatedCar == null)
            {
                return Ok(APIResponseWithError.Create($"Car with id {id} not found"));
            }

            return Ok(APIResponseWithData<GetCarstResponse>.Create(updatedCar));
        }

        // ============================
        // DELETE
        // ============================
        [HttpDelete("{id}")]
        public ActionResult<APIResponse> Delete([FromRoute] int id)
        {
            bool isDeleted = _carService.DeleteAsync(id);

            if (isDeleted)
            {
                return Ok(APIResponseWithData<int>.Create(id));
            }

            return Ok(APIResponseWithError.Create($"The item with id {id} not found"));
        }
    }
}

