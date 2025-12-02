using LibraryManagementSystem.DTOs;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController: ControllerBase
{
    private readonly IBookRepository _bookRepository;

    public BookController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    
    [Authorize(Roles = "User, Admin")]
    [HttpGet("get-all-books")]
    public async Task<IActionResult> GetAllBooks()
    {
        var result = await _bookRepository.GetAllBooksAsync();
        return Ok(result);
    }
    [Authorize(Roles = "User, Admin")]
    [HttpGet("get-book-by-id/{id}")]
    public async Task<IActionResult> GetBookById(int id)
    {
        var result = await _bookRepository.GetBookByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [Authorize(Roles = "User, Admin")]
    [HttpGet("get-book-by-author")]
    public async Task<IActionResult> GetBookByAuthor()
    {
        var booksGroupedByAuthor = await _bookRepository.GetBooksGroupedByAuthor();
        return Ok(booksGroupedByAuthor);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("add-book")]
    public async Task<IActionResult> AddBook([FromBody] CreateBookDTO dto)
    {
        var book = new Book{Author = dto.Author, Title = dto.Title, ISBN = dto.ISBN, CopiesAvailable = dto.CopiesAvailable};
        await _bookRepository.AddBookAsync(book);
        await _bookRepository.SaveChangesAsync();
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("update-book/{id}")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookDTO dto)
    {
        var book = await _bookRepository.GetBookByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        book.Title = dto.Title ?? book.Title;
        book.Author = dto.Author ?? book.Author;
        book.ISBN = dto.ISBN ?? book.ISBN;
        book.CopiesAvailable = dto.CopiesAvailable ?? book.CopiesAvailable;

        _bookRepository.UpdateBookAsync(book);
        await _bookRepository.SaveChangesAsync();
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("delete-book/{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        await _bookRepository.DeleteBookAsync(id);
        await _bookRepository.SaveChangesAsync();
        return Ok();
    }
}