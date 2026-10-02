using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CarsShop.Controllers
{
    [Authorize]
    public abstract class AuthorizedController : ControllerBase
    {
       
    }
}
