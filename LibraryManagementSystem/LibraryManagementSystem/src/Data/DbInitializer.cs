namespace LibraryManagementSystem.Data;

using System;
using System.Linq;
using System.Collections.Generic;
using LibraryManagementSystem.Models;

public static class DbInitializer
{
    public static void Seed(ApplicationDbContext context)
    {
        if (context == null) return;

        try
        {
            // If any data exists, assume DB has been seeded
            if (context.Book.Any() || context.User.Any() || context.Rent.Any())
            {
                return;
            }

            // Seed Users
            var users = new List<User>
            {
                new User { Name = "Alice Johnson", Email = "alice@example.com"},
                new User { Name = "Bob Smith", Email = "bob@example.com" }
            };

            context.User.AddRange(users);
            context.SaveChanges();

            // Seed Books
            var books = new List<Book>
            {
                new Book { Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", ISBN = "4.3", CopiesAvailable = 3 },
                new Book { Title = "1984", Author = "George Orwell", ISBN = "4.5", CopiesAvailable = 4 },
                new Book { Title = "Clean Code", Author = "Robert C. Martin", ISBN = "4.7", CopiesAvailable = 2 },
                new Book { Title = "Clean Code", Author = "Robert C. Martin", ISBN = "4.7", CopiesAvailable = 3 },
                new Book { Title = "Clean Code", Author = "Robert C. Martin", ISBN = "4.7", CopiesAvailable = 5 }
            };

            context.Book.AddRange(books);
            context.SaveChanges();

            // Seed a Rent (borrow record)
            var rents = new List<Rent>
            {
                new Rent { BookId = books[0].Id, UserId = users[0].Id, RentDate = DateTime.UtcNow.AddDays(-7), ReturnDate = null }
            };

            context.Rent.AddRange(rents);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            // Avoid crashing the app during startup; log to console for now.
            Console.WriteLine($"An error occurred seeding the database: {ex.Message}");
        }
    }
}

