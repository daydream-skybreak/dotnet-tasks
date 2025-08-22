namespace mvc_project.Models;

public class RootData
{
    public PersonalDetails Personal_Details { get; set; }
    public string Summary { get; set; }
    public List<Experience> Experience { get; set; }
    public List<Education> Education { get; set; }
    public List<Certification> Certifications { get; set; }
    public Skills Skills { get; set; }
}