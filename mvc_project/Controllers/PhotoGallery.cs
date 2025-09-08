using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace mvc_project.Controllers;

public class PhotoGallery : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}