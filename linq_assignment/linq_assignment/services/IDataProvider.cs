using linq_assignment.models;

namespace linq_assignment.services;

public interface IDataProvider
{
    IEnumerable<Student> GetAllStudents();
    IEnumerable<Course> GetAllCourses();
    IEnumerable<Enrollment> GetAllEnrollments();
}