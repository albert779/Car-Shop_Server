
using CarsShop.Db;
using CarsShop.Db.Models;
using CarsShop.Dto.Requests;
using CarsShop.Interfeces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarsShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TableOfRequestsController : ControllerBase
    {

        private readonly ITableOfRequests _service;
        private readonly AppDbContext _context;

        public TableOfRequestsController(
    ITableOfRequests service,
    AppDbContext context)
        {
            _service = service;
            _context = context;
        }



        [HttpGet]
        public async Task<IActionResult> GetRequests([FromQuery] string? search, int? statusId, DateTime? fromDate,
    DateTime? toDate)
        {
            var result = await _service.GetRequests(search, statusId, fromDate,
     toDate);

            return Ok(result);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRequest(
    int id,
    [FromBody] UpdateRequestDto request)
        {
            var existingRequest = await _context.VehicleRequests
                .FirstOrDefaultAsync(x => x.Id == id);

            if (existingRequest == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Request not found"
                });
            }

            existingRequest.RequestStatusId = request.StatusId;
            existingRequest.LastUpdate = DateTime.UtcNow;


           

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.InnerException?.Message ?? ex.Message
                });
            }

            return Ok(new
            {
                success = true,
                message = "Request updated successfully"
            });
        }


        [HttpPost("message")]
        public async Task<IActionResult> SendMessage(
    [FromBody] CreateMessageDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Message data is required"
                });
            }

            if (string.IsNullOrWhiteSpace(dto.MessageText))
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Message cannot be empty"
                });
            }

            var requestExists = await _context.VehicleRequests
               // .AnyAsync(x => x.Id == dto.RequestId);
                 .FirstOrDefaultAsync(x => x.Id == dto.RequestId);

            if (requestExists == null)
            {
                return BadRequest(new
                {
                    success = false,
                    message = $"Request {dto.RequestId} not found"
                });
            }

            var senderExists = await _context.Users
                .AnyAsync(x => x.Id == dto.SenderId);

            if (!senderExists)
            {
                return BadRequest(new
                {
                    success = false,
                    message = $"Sender {dto.SenderId} not found"
                });
            }

            var receiverExists = await _context.Users
                .AnyAsync(x => x.Id == dto.ReceiverId);

            if (!receiverExists)
            {
                return BadRequest(new
                {
                    success = false,
                    message = $"Receiver {dto.ReceiverId} not found"
                });
            }

            // Use one timestamp for both message and request
            var now = DateTime.UtcNow;

            var newMessage = new Message
            {
                RequestId = dto.RequestId,
                SenderId = dto.SenderId,
                ReceiverId = dto.ReceiverId,
                MessageText = dto.MessageText.Trim(),
                CreatedAt = now
            };

            try
            {
                _context.Messages.Add(newMessage);

                // Update VehicleRequest.LastUpdate
                requestExists.LastUpdate = now;


                await _context.SaveChangesAsync();

                return Ok(new
                {
                    success = true,
                    message = "Message added successfully",

                    data = new
                    {
                        id = newMessage.Id,
                        requestId = newMessage.RequestId,
                        senderId = newMessage.SenderId,
                        receiverId = newMessage.ReceiverId,
                        messageText = newMessage.MessageText,
                        createdAt = newMessage.CreatedAt,
                        
                        lastUpdate = requestExists.LastUpdate
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message,
                    innerException = ex.InnerException?.Message
                });
            }
        }
    }
}