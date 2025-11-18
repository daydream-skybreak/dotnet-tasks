using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using mvc_project.Services;

namespace mvc_project.Controllers;

[ApiController]
[Route("api/userdata")]
public class UserDataController : ControllerBase
{
    private JsonDataService _dataService;
    private ApplicationDbContext _db;

    public UserDataApi(JsonDataService dataService, ApplicationDbContext db)
    {
        _dataService = dataService;
        _db = db;
    }
    [HttpGet("personal-data")]
    public IActionResult Get()
    {
        var personalDetail = _dataService.GetPersonalDetails();
        return Ok(personalDetail);
    }
    
    [HttpPost("change-personal-data")]
    public IActionResult Post([FromBody] Dictionary<string, string> updateDetail)
    {
        var personalDetails = _dataService.GetPersonalDetails();

        _dataService.UpdatePersonalDetails(updateDetail);
    }
}