using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using mvc_project.Models;

namespace mvc_project.Controllers;

public class PhotoGalleryController : Controller
{
    private ApplicationDbContext _db;

    public PhotoGalleryController(ApplicationDbContext db)
    {
        _db = db;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var photos = await _db.Photos.ToListAsync();
        return View(photos);
    }

    [HttpPost]
    public async Task<IActionResult> Index(IFormFile? file)
    {
        Console.WriteLine("form successfully submitted");
        Console.WriteLine($"file: {file}");
        if (file is null || file.Length == 0) return BadRequest();
        
        var path = Path.Combine("images");
        
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var uploadPath = Path.Combine(path, fileName);
        await using var stream = new FileStream(uploadPath, FileMode.Create);  
        await file.CopyToAsync(stream);
        var photo = new Photo
        {
            path = uploadPath,
        };
        _db.Photos.Add(photo);
        await _db.SaveChangesAsync();
        
        
        return Redirect($"PhotoGallery/Index/");    
    }
}