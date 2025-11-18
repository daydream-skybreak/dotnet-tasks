using linq_assignment.models;

namespace linq_assignment.services;

public class MockDataProvider : IDataProvider
{
    public IEnumerable<Student> GetAllStudents() => new List<Student>
    {
        new Student { StudentId = 1, Name = "Alice", Age = 20 },
        new Student { StudentId = 2, Name = "Bob", Age = 22 },
        new Student { StudentId = 3, Name = "Charlie", Age = 23 },
        new Student { StudentId = 4, Name = "David", Age = 24 },
        new Student { StudentId = 5, Name = "Eve", Age = 25 },
    };
    public IEnumerable<Course> GetAllCourses() => new List<Course>
    {
        new Course { CourseId = 1, CourseName = "Mathematics" },
        new Course { CourseId = 2, CourseName = "Physics" },
        new Course { CourseId = 3, CourseName = "Chemistry" },
    };

    public IEnumerable<Enrollment> GetAllEnrollments() => new List<Enrollment>
    {
        new Enrollment { StudentId = 1, CourseId = 1, EnrollmentDate = DateTime.Now.AddMonths(-3), Grade = 85.5 },
        new Enrollment { StudentId = 1, CourseId = 2, EnrollmentDate = DateTime.Now.AddMonths(-2), Grade = 90.0 },
        new Enrollment { StudentId = 2, CourseId = 1, EnrollmentDate = DateTime.Now.AddMonths(-4), Grade = 78.0 },
        new Enrollment { StudentId = 3, CourseId = 3, EnrollmentDate = DateTime.Now.AddMonths(-1), Grade = 88.5 },
        new Enrollment { StudentId = 4, CourseId = 2, EnrollmentDate = DateTime.Now.AddMonths(-5), Grade = 92.0 },
        new Enrollment { StudentId = 5, CourseId = 1, EnrollmentDate = DateTime.Now.AddMonths(-6), Grade = 81.0 },
        new Enrollment {StudentId = 5, CourseId = 3, EnrollmentDate = DateTime.Now.AddMonths(-2), Grade = 79.5 },
    };
}