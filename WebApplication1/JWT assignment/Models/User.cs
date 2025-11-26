namespace WebApplication1.Models;

public class User
{
    public string Username { get; init; } = default!;
    public string Password { get; init; } = null!; // only for demo — use hashed password in prod
    public string[] Roles { get; init; } = Array.Empty<string>();
}