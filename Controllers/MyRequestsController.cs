using CarsShop.Dto.Responses.API;
using CarsShop.Interfeces.Services;
using CarsShop.Services.Auth;
//using CarsShop.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarsShop.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DashboardController : ControllerBase
{
    private readonly IVehicleRequestService _vehicleRequestService;

    public DashboardController(IVehicleRequestService vehicleRequestService)
    {
        _vehicleRequestService = vehicleRequestService;
    }

    [HttpGet]
    public async Task<IActionResult> Dashboard()
    {
        var userIdClaim = User.FindFirst(AuthService.ClaimIdKey);

        if (userIdClaim == null)
        {
            return Unauthorized();
        }

        var userId = int.Parse(userIdClaim.Value);

        var result = await _vehicleRequestService.GetDashboardAsync(userId);

        return Ok(APIResponse.CreateOKWithData(result));
    }
}