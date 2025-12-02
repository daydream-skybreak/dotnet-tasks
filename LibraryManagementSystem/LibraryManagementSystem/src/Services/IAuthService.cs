using LibraryManagementSystem.DTOs;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services;

public interface IAuthService
{
        string GenerateToken(string userId, string username, string role);
        Task<User?> FetchUser(string Email);
        bool VerifyPassword(string password, string storedHash);
        Task<RegisterResult?> RegisterAsync(SignupDTO dto);
}