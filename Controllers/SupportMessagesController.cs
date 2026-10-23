using CarsShop.Dto.Messages;
using CarsShop.Db;
using CarsShop.Db.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CarsShop.Controllers
{
    [ApiController]
    [Route("api/support-messages")]
    [Authorize]
    public class SupportMessagesController : ControllerBase
    {
        private readonly AppDbContext _db;

        public SupportMessagesController(AppDbContext db)
        {
            _db = db;
        }

        private int? GetCurrentUserId()
        {
            var value = User.FindFirst("id")?.Value
                ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return int.TryParse(value, out var id) ? id : null;
        }

        // GET: api/support-messages/requests/5
        [HttpGet("requests/{requestId:int}")]
        public async Task<IActionResult> GetMessages(int requestId)
        {
            var currentUserId = GetCurrentUserId();

            if (currentUserId == null)
                return Unauthorized();

            var request = await _db.VehicleRequests
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == requestId);

            if (request == null)
                return NotFound(new { message = "Request not found." });

            var isManager = User.IsInRole("Manager");

            if (request.UserId != currentUserId.Value && !isManager)
                return Forbid();

            var messages = await _db.Messages
                .AsNoTracking()
                .Where(m => m.RequestId == requestId)
                .OrderBy(m => m.CreatedAt)
                .ThenBy(m => m.Id)
                .Select(m => new MessageDto
                {
                    Id = m.Id,
                    RequestId = m.RequestId,
                    SenderId = m.CreatedByUserId,
                    ReceiverId = m.ReplyedByUserId,
                    MessageText = m.MessageText,
                    CreatedAt = m.CreatedAt
                })
                .ToListAsync();

            return Ok(new
            {
                success = true,
                data = messages
            });
        }

        // POST: api/support-messages
        [HttpPost]
        public async Task<IActionResult> SendMessage(
            [FromBody] CreateMessageDto dto)
        {
            var currentUserId = GetCurrentUserId();

            if (currentUserId == null)
                return Unauthorized();

            if (dto == null ||
                string.IsNullOrWhiteSpace(dto.MessageText))
            {
                return BadRequest(new
                {
                    message = "Message cannot be empty."
                });
            }

            var request = await _db.VehicleRequests
                .FirstOrDefaultAsync(r => r.Id == dto.RequestId);

            if (request == null)
                return NotFound(new { message = "Request not found." });

            var isManager = User.IsInRole("Manager");

            if (request.UserId != currentUserId.Value && !isManager)
                return Forbid();

            int receiverId;

            if (isManager)
            {
                // Manager replies to the request owner.
                receiverId = request.UserId;
            }
            else
            {
                // Find a manager who can receive the message.
                var managerId = await _db.UserToRoles
                    .Where(ur => ur.Role != null &&
                                 ur.Role.Name == "Manager")
                    .Select(ur => (int?)ur.UserId)
                    .FirstOrDefaultAsync();

                if (managerId == null)
                {
                    return BadRequest(new
                    {
                        message = "No manager is available to receive messages."
                    });
                }

                receiverId = managerId.Value;
            }

            var message = new Message
            {
                RequestId = request.Id,
                CreatedByUserId = currentUserId.Value,
                ReplyedByUserId = receiverId,
                MessageText = dto.MessageText.Trim(),
                CreatedAt = DateTime.UtcNow
            };

            _db.Messages.Add(message);
            await _db.SaveChangesAsync();

            var result = new MessageDto
            {
                Id = message.Id,
                RequestId = message.RequestId,
                SenderId = message.CreatedByUserId,
                ReceiverId = message.ReplyedByUserId,
                MessageText = message.MessageText,
                CreatedAt = message.CreatedAt
            };

            return Ok(new
            {
                success = true,
                data = result
            });
        }
    }
}