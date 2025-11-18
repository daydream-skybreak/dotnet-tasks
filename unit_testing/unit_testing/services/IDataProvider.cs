using unit_testing.models;

namespace unit_testing.services;

public interface IDataProvider
{
    IEnumerable<Student> GetAllStudents();
    IEnumerable<Course> GetAllCourses();
    IEnumerable<Enrollment> GetAllEnrollments();
}