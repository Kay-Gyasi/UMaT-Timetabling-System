using UMaTLMS.Core.Repositories.Base;

namespace UMaTLMS.Core.Repositories;

public interface IExamsScheduleRepository : IRepository<ExamsSchedule, int>
{
    Task<List<ExamsSchedule>> GetAll();
}