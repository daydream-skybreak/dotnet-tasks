using System.Security.Claims;
using System.Text;
using LibraryManagementSystem.Helpers;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using LibraryManagementSystem.DTOs;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using Microsoft.IdentityModel.Tokens;


namespace LibraryManagementSystem.Services;

public class AuthService: IAuthService
{
    private readonly JwtSettings _settings;
    private readonly IUserRepository _userRepository;
    public AuthService(IOptions<JwtSettings> settings, IUserRepository userRepository)
    {
        _settings = settings.Value;
        _userRepository = userRepository;
    }
    public bool VerifyPassword(string password, string storedHash)
    {
        var newPasswordHash = CreatePasswordHash(password);
        return newPasswordHash == storedHash;
    }

    private string CreatePasswordHash(string password)
    {
        var hashedPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
        return hashedPassword;
    }
    public string GenerateToken(string userId, string username, string role)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.UniqueName, username),
            new Claim("role", role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_settings.ExpiresMinutes),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    public async Task<User?> FetchUser(string email)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        return user;
    }
    public async Task<RegisterResult?> RegisterAsync(SignupDTO dto)
    {
        var exists = await _userRepository.GetUserByEmailAsync(dto.Email);
        if (exists != null)
            return RegisterResult.ExistsError();

        var hashed = CreatePasswordHash(dto.Password);

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            Password = hashed,
            Role = dto.Role
        };

        await _userRepository.AddUserAsync(user);
        await _userRepository.SaveChangesAsync();

        return RegisterResult.SuccessResult(user);
    }
}

public class RegisterResult
{
    public bool Success { get; set; }
    public string? Error { get; set; }
    public User? User { get; set; }

    public static RegisterResult ExistsError()
        => new RegisterResult { Success = false, Error = "exists" };

    public static RegisterResult SuccessResult(User user)
        => new RegisterResult { Success = true, User = user };
}