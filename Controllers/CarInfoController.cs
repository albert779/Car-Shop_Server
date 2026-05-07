
using CarsShop.Db;
using CarsShop.Interfeces.Services;
using CarsShop.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace CarsShop.Api.Controllers
{
    [ApiController]
    [Route("api/request")]
    public class RequestInfoController : ControllerBase
    {
        private readonly IRequestSubmitterService _requestSubmitterService;
       // private readonly EmailService _emailService;
        private readonly AppDbContext _context;

        public RequestInfoController(
            IRequestSubmitterService requestSubmitterService,
          //  EmailService emailService,
            AppDbContext context)
        {
            _requestSubmitterService = requestSubmitterService;
            //_emailService = emailService;
            _context = context;
        }
        

        
        [HttpPost]
        public async Task<IActionResult> CreateRequest([FromBody] VehicleRequestCreateDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data");
            var userClaim = this.User.Claims.Single(claim => claim.Type == AuthService.ClaimIdKey);
            var userIdString = userClaim.Value;
            var userId = int.Parse(userIdString);
            try
            {
                // ✅ Save to DB
                var savedRequest = await _requestSubmitterService.AddNew(dto, userId);

                // ✅ Load full data from DB
                /*
                var fullRequest = await _context.CarInfoRequests
                    .Include(r => r.Car)
                    .Include(r => r.User)
                    .FirstOrDefaultAsync(r => r.Id == savedRequest.Id);
                */

                //await _emailService.SendFromRequest(savedRequest);

                if (savedRequest == null)
                   return StatusCode(500, "Failed to save request");
                //return NotFound();

                // ✅ Send email using DB values
                /*
                await _emailService.SendEmail(
                    fullRequest.User?.Name ?? "",
                    fullRequest.User?.LastName ?? "",
                    fullRequest.User?.Phone ?? "",
                    fullRequest.User?.Email ?? "",
                    fullRequest.Car?.Model ?? "",
                    fullRequest.Car?.Color ?? "",
                    fullRequest.Car?.Price ?? 0,
                    fullRequest.Message
                );
                */

                // ✅ Load full data (important for email)
                //var fullRequest = await _context.CarInfoRequests
                //    .Include(r => r.Car)
                //    .Include(r => r.User)
                //    .FirstOrDefaultAsync(r => r.Id == savedRequest.Id);

                //if (fullRequest == null)
                //    return StatusCode(500, "Request not found after saving");

                //await _emailService.SendFromRequest(fullRequest);

                return Ok(new { message = "Saved + Email sent" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return StatusCode(500, new
                {
                    message = ex.Message,
                    stack = ex.StackTrace
                });
            }
        }
    }
}
        

/*
using CarsShop.Interfeces.Services;
using CarsShop.RequestsDto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CarsShop.Api.Controllers
{
    [ApiController]
    [Route("api/request")]
    public class RequestInfoController : ControllerBase
    {
        private readonly IRequestSubmitterService _requestSubmitterService;
        private readonly EmailService _emailService;

        public RequestInfoController(
            IRequestSubmitterService requestSubmitterService,
            EmailService emailService)
        {
            _requestSubmitterService = requestSubmitterService;
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRequest([FromBody] CreateRequestDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data");

            try
            {
                // ✅ Save + return full mapped entity
                var savedRequest = await _requestSubmitterService.AddNew(dto);

                // ⚠️ IMPORTANT: make sure DTO already contains needed values
                await _emailService.SendEmail(
                    dto.FirstName,
                    dto.LastName,
                    dto.Phone,
                    dto.Email,
                    dto.Model,
                    dto.Color,
                    dto.Price,
                    dto.Details
                );

                return Ok(new { message = "Saved + Email sent" });
            }
            catch (Exception ex)
            {
                // 🔥 IMPORTANT: expose real error while debugging
                return StatusCode(500, ex.ToString());
            }
        }
    }
}
*/