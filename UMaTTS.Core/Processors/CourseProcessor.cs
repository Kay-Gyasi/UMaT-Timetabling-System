using Humanizer;
using System.Text.Json;
using UMaTLMS.Core.Helpers;
using UMaTLMS.Core.Repositories;

namespace UMaTLMS.Core.Processors;

[Processor]
public class CourseProcessor
{
    private readonly ICourseRepository _courseRepository;
    private readonly ILectureRepository _lectureRepository;
    private readonly IPreferenceRepository _preferenceRepository;

    public CourseProcessor(ICourseRepository courseRepository, ILectureRepository lectureRepository,
        IPreferenceRepository preferenceRepository)
    {
        _courseRepository = courseRepository;
        _lectureRepository = lectureRepository;
        _preferenceRepository = preferenceRepository;
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

    public async Task<PaginatedList<PreferenceDto>> GetPreferences(PaginatedCommand command)
    {
        var preferences = await _preferenceRepository.GetCoursePreferences(command);
        List<PreferenceDto> dtoData = new();
        foreach (var data in preferences.Data)
        {
            var value = string.Empty;
            switch (data.Type)
            {
                case PreferenceType.DayNotAvailable:
                    var p1 = JsonSerializer.Deserialize<DayNotAvailable>(data.Value);
                    if (p1 is null) break;
                    value = p1.Day.Humanize();
                    break;
                case PreferenceType.TimeNotAvailable:
                    var p2 = JsonSerializer.Deserialize<TimeNotAvailable>(data.Value);
                    if (p2 is null) break;
                    value = string.Join(":", p2.Day.Humanize(), p2.Time);
                    break;
                case PreferenceType.PreferredDayOfWeek:
                    var p3 = JsonSerializer.Deserialize<PreferredDayOfWeek>(data.Value);
                    if (p3 is null) break;
                    value = p3.Day.Humanize();
                    break;
                case PreferenceType.PreferredLectureRoom:
                    var p4 = JsonSerializer.Deserialize<PreferredLectureRoom>(data.Value);
                    if (p4 is null) break;
                    value = p4.Room.Humanize();
                    break;
                default:
                    break;
            }

            dtoData.Add(new PreferenceDto(data.Id, data.Type.Humanize(), value,
                            data.TimetableType.Humanize(), null, data.Course!.Name));
        }

        return new PaginatedList<PreferenceDto>(dtoData, preferences.TotalCount, preferences.CurrentPage, preferences.PageSize);
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