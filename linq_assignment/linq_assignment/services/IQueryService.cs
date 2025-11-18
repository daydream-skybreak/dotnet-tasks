using linq_assignment.models;

namespace linq_assignment.services;

public interface IQueryService
{
    IQueryable<object> GetJoinedData();
    IQueryable<object> GetAverageGradesPerCourse();
    IQueryable<Student> FilterStudentsById(int id);
}