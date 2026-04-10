using CarsShop.Db;
using CarsShop.RequestsDto;
using CarsShop.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarsShop.Api.Controllers
{
    [ApiController]
    [Route("api/request")]
    public class RequestInfoController : ControllerBase
    {
        private readonly IRequestInfoService _service;
        //private object _emailService;
        private readonly EmailService _emailService;

        public RequestInfoController(IRequestInfoService service)
        {
            _service = service;
        }


        

        [HttpPost]
        public async Task<IActionResult> SendRequest([FromBody] RequestInfoDto request)
        {

            if (request == null)
            {
                return BadRequest("Invalid request data.");
            }

            try
            {
                await _emailService.SendEmail(
                    request.FirstName,
                    request.LastName,
                    request.Phone,
                    request.Email,
                    request.Details
                );

                return Ok(new { message = "Request sent successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to send request.", error = ex.Message });
            }
        }
    }
}