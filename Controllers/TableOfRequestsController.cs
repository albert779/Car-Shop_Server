
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
    public class TableOfRequestsController : AuthorizedController
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
            if (request == null)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Request data is required"
                });
            }

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

            // Update only the request status
            existingRequest.RequestStatusId = request.StatusId;

            // Update last modification time
            existingRequest.LastUpdate = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    success = true,
                    message = "Request updated successfully",
                    data = new
                    {
                        requestId = existingRequest.Id,
                        statusId = existingRequest.RequestStatusId,
                        lastUpdate = existingRequest.LastUpdate
                    }
                });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Database error while updating request",
                    error = ex.InnerException?.Message ?? ex.Message
                });
            }
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

            if (dto.RequestId <= 0)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid request ID"
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

            // Get logged-in user ID from JWT
            var senderId= GetUserId();

           // Find the vehicle request
            var request = await _context.VehicleRequests
                .FirstOrDefaultAsync(x => x.Id == dto.RequestId);

            if (request == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"Request {dto.RequestId} not found"
                });
            }

            // The receiver is the user who created the request.
            // Do NOT take receiverId from Angular.
            var receiverId = request.UserId;

            if (receiverId <= 0)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Receiver ID is invalid"
                });
            }

            var now = DateTime.UtcNow;

            var newMessage = new Message
            {
                RequestId = request.Id,
                SenderId = senderId,
                ReceiverId = receiverId,
                MessageText = dto.MessageText.Trim(),
                CreatedAt = now
            };

            try
            {
                _context.Messages.Add(newMessage);

                // Update the request's last activity time
                request.LastUpdate = now;

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
                        lastUpdate = request.LastUpdate
                    }
                });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Database error while saving message",
                    error = ex.InnerException?.Message ?? ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Failed to save message",
                    error = ex.Message
                });
            }
        }
    }
}