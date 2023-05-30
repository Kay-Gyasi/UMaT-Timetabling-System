using UMaTLMS.Core.Contracts;
using UMaTLMS.Core.Processors;

namespace UMaTLMS.Core.Services;

public interface IUMaTApiService
{
    Task<List<Staff>?> GetLecturers();
    Task<List<CourseResponse>?> GetCourses();
    Task<List<Group>?> GetClasses();
    Task<List<DepartmentResponse>?> GetDepartments();
}