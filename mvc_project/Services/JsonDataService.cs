using System.Text.Json;

namespace mvc_project.Services;

public class JsonDataService
{
    private readonly RootData _rootData;

    public JsonDataService(IWebHostEnvironment env)
    {
        var filePath = Path.Combine(env.ContentRootPath, "wwwroot", "personal_file.json");
        var json = File.ReadAllText(filePath);
        _rootData = JsonSerializer.Deserialize<RootData>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? throw new NullReferenceException("Failed to serialize JSON file");
    }

    public PersonalDetails GetPersonalDetails() => _rootData.Personal_Details;
    public string GetSummary() => _rootData.Summary;
    public List<Experience> GetExperiences() => _rootData.Experience;
    public List<Education> GetEducation() => _rootData.Education;
    public List<Certification> GetCertifications() => _rootData.Certifications;
    public Skills GetSkills() => _rootData.Skills;
}

public class RootData
{
    public PersonalDetails Personal_Details { get; set; }
    public string Summary { get; set; }
    public List<Experience> Experience { get; set; }
    public List<Education> Education { get; set; }
    public List<Certification> Certifications { get; set; }
    public Skills Skills { get; set; }
}

public class PersonalDetails
{
    public string Name { get; set; }
    public string Title { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Location { get; set; }
    public string Linkedin { get; set; }
    public string Github { get; set; }
}

public class Experience
{
    public string Title { get; set; }
    public string Company { get; set; }
    public string Start_Date { get; set; }
    public string End_Date { get; set; }
    public List<string> Responsibilities { get; set; }

    // ⚠️ "tech_used" can be string OR array → safest to use List<string>
    public List<string> Tech_Used { get; set; }  
    // If you want stricter handling, we’d write a custom JsonConverter 
    // to always normalize to List<string>
}

public class Education
{
    public string Degree { get; set; }
    public string School { get; set; }
    public string Start_Year { get; set; }
    public string End_Year { get; set; }
    public string Grade { get; set; }
}

public class Certification
{
    public string Name { get; set; }
    public string Organization { get; set; }
}

public class Skills
{
    public List<string> Programming_Languages { get; set; }
    public List<string> Frontend { get; set; }
    public List<string> Backend { get; set; }
    public List<string> Database { get; set; }
    public List<string> Version_Control { get; set; }
    public List<string> Containerization { get; set; }
    public List<string> Soft_Skills { get; set; }
}
