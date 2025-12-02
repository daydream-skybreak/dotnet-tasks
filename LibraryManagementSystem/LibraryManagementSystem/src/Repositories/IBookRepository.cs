using LibraryManagementSystem.DTOs;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Helpers;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Repositories;

public interface IBookRepository
{
    Task<List<Book>> GetAllBooksAsync();
    Task<Book?> GetBookByIdAsync(int id);
    Task<List<BooksByAuthorDTO>> GetBooksGroupedByAuthor();
    Task AddBookAsync(Book book);
    EntityState UpdateBookAsync(Book book);
    Task DeleteBookAsync(int id);
    Task<PagedResult<Book>> GetPagedBooksAsync(string? s, int pageNumber, int pageSize);
    Task SaveChangesAsync();
}