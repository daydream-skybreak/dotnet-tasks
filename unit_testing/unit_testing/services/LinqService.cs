using unit_testing.models;

namespace unit_testing.services;

public class LinqService : IQueryService
{
    private readonly IEnumerable<Student> _students;
    private readonly IEnumerable<Enrollment> _enrollments;
    private readonly IEnumerable<Course> _courses;

    public LinqService(IDataProvider dataProvider)
    {
        _students = dataProvider.GetAllStudents();
        _enrollments = dataProvider.GetAllEnrollments();
        _courses = dataProvider.GetAllCourses();
    }
    public IQueryable<object> GetJoinedData()
    {
        var data = from e in _enrollments
            join s in _students on e.StudentId equals s.StudentId
            join c in _courses on e.CourseId equals c.CourseId
            select new
            {
                s.Name,
                c.CourseName,
                e.EnrollmentDate,
                e.Grade,
                Status = e.Grade < 50 ? "Fail" : "Pass"
            };
        return data.AsQueryable();    
    }
    public IQueryable<object> GetAverageGradesPerCourse()
    {
        var data = from e in _enrollments
            group e by e.CourseId into courseGroup
            join c in _courses on courseGroup.Key equals c.CourseId
            select new
            {
                c.CourseName,
                AverageGrade = courseGroup.Average(e => e.Grade)
            };
        return data.AsQueryable();
    }

    public IQueryable<Student> FilterStudentsById(int courseId)
    {
        var student = from s in _students
                select new Student();
        return student.AsQueryable();
    }
}