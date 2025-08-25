using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mvc_project.Models;
using mvc_project.Services;

namespace mvc_project.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly JsonDataService _jsonData;

    public HomeController(ILogger<HomeController> logger,  JsonDataService jsonData)
    {
        _logger = logger;
        _jsonData = jsonData;
    }

    public IActionResult Index()
    {
        return View(_jsonData);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}