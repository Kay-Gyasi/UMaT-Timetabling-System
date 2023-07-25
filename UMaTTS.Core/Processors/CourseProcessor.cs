using UMaTLMS.Core.Helpers;

namespace UMaTLMS.Core.Processors;

[Processor]
public class CourseProcessor
{
    private readonly ICourseRepository _courseRepository;
    private readonly ILectureRepository _lectureRepository;

    public CourseProcessor(ICourseRepository courseRepository, ILectureRepository lectureRepository)
    {
        _courseRepository = courseRepository;
        _lectureRepository = lectureRepository;
    }

    public async Task<OneOf<bool, Exception>> UpdateAsync(CourseCommand command)
    {
        var courses = await _courseRepository.GetAllAsync(x => x.Name == command.Name);
        if (courses is null) return new NullReferenceException();

        foreach (var course in courses)
        {
            course.MarkAsNotExaminable(command.IsExaminable)
                .HasNoWeeklyLectures(command.IsToHaveWeeklyLectureSchedule)
                .MarkAsHavingPracticalExams(command.HasPracticalExams)
                .WithHours(command.TeachingHours, command.PracticalHours);

            var lecturesForCourse = await _lectureRepository.GetAllAsync(x => x.CourseId == course.Id);
            foreach (var lecture in lecturesForCourse)
            {
                if (lecture.IsPractical)
                {
                    lecture.SetDuration(command.PracticalHours);
                    continue;
                }

                lecture.SetDuration(command.TeachingHours);
                await _lectureRepository.UpdateAsync(lecture, saveChanges: false);
            }

            await _courseRepository.UpdateAsync(course, saveChanges: false);
        }

        return await _courseRepository.SaveChanges();
    }

    public async Task<OneOf<CourseDto, Exception>> GetAsync(int id)
    {
        var course = await _courseRepository.FindByIdAsync(id, useCache: true);
        if (course is null) return new NullReferenceException();

        return new CourseDto(course.Id, course.Code, course.Name ?? string.Empty, course.Credit, course.TeachingHours, 
            course.PracticalHours, course.IsExaminable, course.IsToHaveWeeklyLectureSchedule, course.HasPracticalExams);
    }

    public async Task<PaginatedList<CourseDto>> GetPageAsync(PaginatedCommand command)
    {
        var page = await _courseRepository.GetPageAsync(command);
        return page.Adapt<PaginatedList<CourseDto>>(Mapping.GetTypeAdapterConfig());
    }

    public async Task DeleteAsync(int id)
    {
        var course = await _courseRepository.FindByIdAsync(id);
        if (course is null) return;

        await _courseRepository.SoftDeleteAsync(course);
    }

    public async Task HardDeleteAsync(int id)
    {
        var course = await _courseRepository.FindByIdAsync(id);
        if (course is null) return;

        await _courseRepository.DeleteAsync(course);
    }
}

public record CourseCommand(string Name, int TeachingHours, int PracticalHours, 
    bool IsExaminable, bool IsToHaveWeeklyLectureSchedule, bool HasPracticalExams);

public record CourseDto(int Id, string? Code, string Name, int Credit, int TeachingHours, 
    int PracticalHours, bool IsExaminable, bool IsToHaveWeeklyLectureSchedule, bool HasPracticalExams);