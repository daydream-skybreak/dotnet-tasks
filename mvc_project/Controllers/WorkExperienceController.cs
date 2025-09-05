using Microsoft.AspNetCore.Mvc;
using mvc_project.Services;

namespace mvc_project.Controllers;

public class WorkExperienceController : Controller
{
    private JsonDataService _dataService;

    public WorkExperienceController(JsonDataService dataService)
    {
        _dataService = dataService;
    }
    // GET
    public IActionResult Index()
    {
        var experiences = _dataService.GetExperiences();
        return View(experiences);
    }

    public IActionResult Details(int id)
    {
        return View();
    }
}