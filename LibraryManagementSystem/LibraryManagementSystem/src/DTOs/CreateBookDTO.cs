namespace LibraryManagementSystem.DTOs;

public class CreateBookDTO
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public int CopiesAvailable { get; set; }
}