
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
        private readonly IUpdateRequestService _updateService;
        private readonly ISendRequestService _sendService;

        public TableOfRequestsController(
            ITableOfRequests service,
            IUpdateRequestService updateService,
            ISendRequestService sendService)
        {
            _service = service;
            _updateService = updateService;
            _sendService = sendService;
        }

        // GET: api/TableOfRequests
        [HttpGet]
        public async Task<IActionResult> GetRequests(
            [FromQuery] string? search,
            int? statusId,
            DateTime? fromDate,
            DateTime? toDate)
        {
            var result = await _service.GetRequests(
                search,
                statusId,
                fromDate,
                toDate);

            return Ok(result);
        }

        // PUT: api/TableOfRequests/{id}
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

            try
            {
                var updatedRequest =
                    await _updateService.UpdateRequestAsync(
                        id,
                        request);

                if (updatedRequest == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Request not found"
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "Request updated successfully",
                    data = new
                    {
                        requestId = updatedRequest.Id,
                        statusId = updatedRequest.RequestStatusId,
                        lastUpdate = updatedRequest.LastUpdate
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

        // POST: api/TableOfRequests/message
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

            var senderId = GetUserId();

            if (senderId <= 0)
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "Invalid sender ID"
                });
            }

            try
            {
                var newMessage =
                    await _sendService.SendMessageAsync(
                        dto,
                        senderId);

                if (newMessage == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = $"Request {dto.RequestId} not found"
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "Message added successfully",
                    data = new
                    {
                        id = newMessage.Id,
                        requestId = newMessage.RequestId,
                        senderId = newMessage.CreatedByUserId,
                        receiverId = newMessage.ReplyedByUserId,
                        messageText = newMessage.MessageText,
                        createdAt = newMessage.CreatedAt,
                        lastUpdate = newMessage.Request.LastUpdate
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
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
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