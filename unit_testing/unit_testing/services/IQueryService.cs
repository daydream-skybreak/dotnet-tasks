namespace unit_testing.services;
using unit_testing.models;

public interface IQueryService
{
    IQueryable<object> GetJoinedData();
    IQueryable<object> GetAverageGradesPerCourse();
    IQueryable<Student> FilterStudentsById(int id);
}