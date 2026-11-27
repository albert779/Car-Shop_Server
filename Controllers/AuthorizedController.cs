

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarsShop.Controllers
{
    [Authorize]
    public abstract class AuthorizedController : ControllerBase
    {
        protected int GetUserId()
        {

            foreach (var claim in User.Claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type} | Value: {claim.Value}");
            }

            Console.WriteLine($"IsAuthenticated: {User.Identity?.IsAuthenticated}");


            var userId =
                User.FindFirst("id")?.Value ??
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userId, out int id))
            {
                throw new UnauthorizedAccessException(
                    "User ID was not found in the authentication token.");
            }

            return id;
        }
    }
}