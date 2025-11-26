namespace WebApplication1.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Models;
using WebApplication1.DTOs;


[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IEnumerable<User> _users;

    public AuthController(IConfiguration config, IEnumerable<User> users)
    {
        _config = config;
        _users = users;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto login)
    {
        var user = _users.SingleOrDefault(u =>
            u.Username.Equals(login.Username, StringComparison.OrdinalIgnoreCase)
            && u.Password == login.Password); // demo only: compare plaintext

        if (user == null)
            return Unauthorized(new { message = "Invalid credentials" });

        var jwtSection = _config.GetSection("Jwt");
        var key = jwtSection.GetValue<string>("Key")!;
        var issuer = jwtSection.GetValue<string>("Issuer")!;
        var audience = jwtSection.GetValue<string>("Audience")!;
        var durationMinutes = jwtSection.GetValue<int?>("DurationMinutes") ?? 60;

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(ClaimTypes.Name, user.Username),
        };

        foreach (var role in user.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(durationMinutes),
            signingCredentials: creds
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return Ok(new
        {
            token = tokenString,
            expires = token.ValidTo,
            username = user.Username,
            roles = user.Roles
        });
    }
}
