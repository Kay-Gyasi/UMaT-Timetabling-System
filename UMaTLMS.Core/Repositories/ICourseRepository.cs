using UMaTLMS.Core.Repositories.Base;

namespace UMaTLMS.Core.Repositories;

public interface ICourseRepository : IRepository<IncomingCourse, int>
{
    Task<List<IncomingCourse>> GetAll();
}