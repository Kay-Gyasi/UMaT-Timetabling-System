using UMaTLMS.Core.Repositories.Base;

namespace UMaTLMS.Core.Repositories;

public interface ILectureScheduleRepository : IRepository<LectureSchedule, int>
{
    Task<int> GetNumberOfLecturesForLecturerInADay(int lecturerId, int day);
}
