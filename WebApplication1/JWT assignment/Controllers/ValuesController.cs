namespace WebApplication1.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class ValuesController : ControllerBase
{
    // Any authenticated user
    [HttpGet("profile")]
    [Authorize]
    public IActionResult Profile()
    {
        var name = User.Identity?.Name ?? "unknown";
        var roles = User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Role).Select(c => c.Value).ToArray();
        return Ok(new { message = $"Hello {name}", roles });
    }

    // Admin only
    [HttpGet("admin")]
    [Authorize(Roles = "Admin")]
    public IActionResult AdminOnly()
    {
        return Ok(new { message = "Admin access granted" });
    }

    // Multiple roles allowed (User or Manager)
    [HttpGet("user-or-manager")]
    [Authorize(Roles = "User,Manager")]
    public IActionResult UserOrManager()
    {
        return Ok(new { message = "User or Manager allowed" });
    }
}
