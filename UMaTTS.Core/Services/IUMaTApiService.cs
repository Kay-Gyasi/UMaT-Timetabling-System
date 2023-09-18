using UMaTLMS.Core.Contracts;
using UMaTLMS.Core.Processors;

namespace UMaTLMS.Core.Services;

public interface IUMaTApiService
{
    Task<List<CourseResponse>?> GetCourses();
    Task<List<Group>?> GetClasses();
}