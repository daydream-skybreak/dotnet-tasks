using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly IAuthService _auth;
    private readonly ApplicationDbContext _db;

    public AuthController(IAuthService auth, ApplicationDbContext db)
    {
        _auth = auth;
        _db = _db;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDTO login)
    {
        var user = _auth.FetchUser(login.Email).Result;

        if (!_auth.VerifyPassword(login.Password, user.Password)) return Unauthorized();
        var token = _auth.GenerateToken($"{user.Id}", user.Name, user.Role.ToString());
        return Ok(new { token });
    }
    
    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] SignupDTO dto)
    {
        var result = await _auth.RegisterAsync(dto);

        if (!result.Success)
        {
            if (result.Error == "exists")
                return Conflict(new { message = "Email already exists." });

            return BadRequest(new { message = "Registration failed." });
        }

        return CreatedAtAction(nameof(Signup), new {
            id = result.User!.Id,
            email = result.User.Email,
            role = result.User.Role.ToString()
        });
    }
}


