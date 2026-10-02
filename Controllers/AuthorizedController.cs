using CarsShop.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


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
