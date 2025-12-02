using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Repositories;

public class UserRepository: IUserRepository
{
    private readonly ApplicationDbContext _db;
    public UserRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public async Task<User> AddUserAsync(User user)
    {
        await _db.User.AddAsync(user);
        return user;
    }
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _db.User.FirstOrDefaultAsync(u => u.Email == email);
    }
    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
}