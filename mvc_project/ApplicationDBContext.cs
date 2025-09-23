using Microsoft.EntityFrameworkCore;
using mvc_project.Models;
namespace mvc_project;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Photo> Photos { get; set; }
}