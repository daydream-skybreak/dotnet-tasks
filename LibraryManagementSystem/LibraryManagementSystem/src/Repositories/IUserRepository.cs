using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByEmailAsync(string membershipNumber);
    Task<User> AddUserAsync(User user);
    Task SaveChangesAsync();
}