/*
using CarsShop.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
//using CarsShop.Services.Interfaces;
using CarsShop.Services.Auth;


namespace CarsShop.Controllers
{
    [Authorize]
    public abstract class AuthorizedController : ControllerBase
    {

       public int GetUserId()
        {
            string userClaimId= User.FindFirstValue(AuthService.ClaimIdKey);
            if (userClaimId == null)
                return -1;

            return Convert.ToInt32(userClaimId);

        }

    }
}
*/

using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace CarsShop.Controllers
{
    public class AuthorizedController : ControllerBase
    {
        protected int GetUserId()
        {
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