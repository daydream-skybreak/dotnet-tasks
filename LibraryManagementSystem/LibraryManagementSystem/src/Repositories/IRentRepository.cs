using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories;

public interface IRentRepository
{
    Task<List<Rent>> GetAllRentsAsync();
    Task<Rent?> UpdateRentAsync(int id);
}