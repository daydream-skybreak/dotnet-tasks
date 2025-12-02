namespace LibraryManagementSystem.DTOs;

public class BooksByAuthorDTO
{
    public string Author { get; set; } = "";
    public List<BookDTO> Books { get; set; } = new();
}