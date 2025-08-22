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
        List<Experience> experiences = _dataService.GetExperiences();
        return View(experiences);
    }
}