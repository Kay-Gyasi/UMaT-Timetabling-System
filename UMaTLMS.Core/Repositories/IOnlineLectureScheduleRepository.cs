using UMaTLMS.Core.Repositories.Base;

namespace UMaTLMS.Core.Repositories;

public interface IOnlineLectureScheduleRepository : IRepository<OnlineLectureSchedule, int>
{
    Task<List<OnlineLectureSchedule>> GetAll();
}