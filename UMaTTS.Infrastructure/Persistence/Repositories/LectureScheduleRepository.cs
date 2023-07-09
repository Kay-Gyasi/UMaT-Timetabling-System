using UMaTLMS.SharedKernel.Helpers;

namespace UMaTLMS.Infrastructure.Persistence.Repositories;

public class LectureScheduleRepository : Repository<LectureSchedule, int>, ILectureScheduleRepository
{
    public LectureScheduleRepository(AppDbContext context, ILogger<LectureScheduleRepository> logger) : base(context, logger)
    {
    }

    public async Task<int> GetNumberOfLecturesForLecturerInADay(int lecturerId, int day)
    {
        return await GetBaseQuery().CountAsync(x =>
            x.DayOfWeek == AppHelper.GetDayOfWeek(day) && (x.FirstLecture!.LecturerId == lecturerId || x.SecondLecture!.LecturerId == lecturerId));
    }

    protected override IQueryable<LectureSchedule> GetBaseQuery()
    {
        return base.GetBaseQuery()
            .Include(x => x.Room)
            .Include(x => x.FirstLecture)
            .Include(x => x.SecondLecture);
    }
}
