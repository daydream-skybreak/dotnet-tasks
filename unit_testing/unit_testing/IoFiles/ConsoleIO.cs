namespace unit_testing.IoFiles;
using unit_testing.services;

public class ConsoleIO
{
    private readonly IQueryService _queryService;

    public ConsoleIO(IQueryService queryService)
    {
        _queryService = queryService;
    }
    
    public void Run()
    {
        Console.WriteLine("=== Advanced LINQ (SOLID) Demo ===");

        DisplayJoinedData();
        DisplayAverageGradesPerCourse();
        GetStudentData();
    }
    
    private void DisplayJoinedData()
    {
        var joinedData = _queryService.GetJoinedData();
        Console.WriteLine("StudentId | StudentName | CourseName | Grade");
        foreach (var item in joinedData)
        {
            Console.WriteLine(item);
        }
    }
    
    private void DisplayAverageGradesPerCourse()
    {
        var avgGrades = _queryService.GetAverageGradesPerCourse();
        foreach (var item in avgGrades)
        {
            Console.WriteLine(item);
        }
    }

    private void GetStudentData()
    {
        Console.Write("Enter Student ID: ");
        if (int.TryParse(Console.ReadLine(), out int studentId))
        {
            var studentData = _queryService.FilterStudentsById(studentId);
            foreach (var student in studentData)
            {
                Console.WriteLine(student);
            }
        }
        else
        {
            Console.WriteLine("Invalid Student ID.");
        }
    }
}