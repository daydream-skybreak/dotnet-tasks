using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs;
using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Repositories;

public class BookRepository: IBookRepository
{
    private readonly ApplicationDbContext _db;
    public BookRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    
    Task<List<Book>> IBookRepository.GetAllBooksAsync()
    {
        return _db.Book.ToListAsync();
    }
    
    Task<Book?> IBookRepository.GetBookByIdAsync(int id)
    {
        return _db.Book.FirstOrDefaultAsync(b => b.Id == id);
    }
    
    async Task<PagedResult<Book>> IBookRepository.GetPagedBooksAsync(string? s, int pageNumber, int pageSize)
    {
        var query = _db.Book.Include(b => b.Author).AsQueryable();

        if (!string.IsNullOrWhiteSpace(s))
            query = query.Where(b => b.Title.Contains(s)); // EF translates

        var total = await query.CountAsync();
        var items = await query
            .OrderBy(b => b.Title)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Book>(items, total, pageSize, pageNumber);
    }

    async Task<List<BooksByAuthorDTO>> IBookRepository.GetBooksGroupedByAuthor()
    {
        return await _db.Book
            .GroupBy(b => b.Author)
            .Select(g => new BooksByAuthorDTO
            {
                Author = g.Key,
                Books = g.Select(b => new BookDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    ISBN = b.ISBN
                }).ToList()
            })
            .ToListAsync();
    }
    
    async Task IBookRepository.AddBookAsync(Book book)
    {
        await _db.Book.AddAsync(book);
        
    }

    EntityState IBookRepository.UpdateBookAsync(Book book)
    {
         var result = _db.Book.Update(book);
         return result.State;
    }
    
    async Task IBookRepository.DeleteBookAsync(int id)
    {
        var book = await _db.Book.FirstOrDefaultAsync(b => b.Id == id);
        if (book != null)
        {
            _db.Book.Remove(book);
        }
    }

    async Task IBookRepository.SaveChangesAsync()
    {
       await _db.SaveChangesAsync();
    }
}