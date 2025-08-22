namespace mvc_project.Models;

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
