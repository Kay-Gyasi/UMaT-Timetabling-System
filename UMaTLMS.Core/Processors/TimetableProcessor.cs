using UMaTLMS.Core.Contracts;
using UMaTLMS.Core.Entities;
using UMaTLMS.Core.Helpers;
using UMaTLMS.Core.Services;

namespace UMaTLMS.Core.Processors;

[Processor]
public class TimetableProcessor
{
    private readonly ILogger<TimetableProcessor> _logger;
    private readonly ILectureScheduleRepository _lectureScheduleRepository;
    private readonly ILectureRepository _lectureRepository;
    private readonly ILecturerRepository _lecturerRepository;
    private readonly IClassGroupRepository _classGroupRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly ISubClassGroupRepository _subClassGroupRepository;
    private readonly IUMaTApiService _umatApiService;

    public TimetableProcessor(ILogger<TimetableProcessor> logger, ILectureScheduleRepository timetableRepository, 
        ILectureRepository lectureRepository, ILecturerRepository lecturerRepository, IClassGroupRepository classGroupRepository, 
        ICourseRepository courseRepository, ISubClassGroupRepository subClassGroupRepository, IUMaTApiService uMaTApiService)
    {
        _logger = logger;
        _lectureScheduleRepository = timetableRepository;
        _lectureRepository = lectureRepository;
        _lecturerRepository = lecturerRepository;
        _classGroupRepository = classGroupRepository;
        _courseRepository = courseRepository;
        _subClassGroupRepository = subClassGroupRepository;
        _umatApiService = uMaTApiService;
    }

    public async Task Generate()
    {
        var lectures = await _lectureRepository.GetAll();
        var schedules = await _lectureScheduleRepository.GetAll();

        schedules = TimetableGenerator.Generate(schedules, lectures);
        foreach (var schedule in schedules)
        {
            await _lectureScheduleRepository.UpdateAsync(schedule, false).ConfigureAwait(false);
        }

        await _lectureScheduleRepository.SaveChanges();
    }

    public async Task<IEnumerable<TimetableDto>> GetClassSchedule(int classId)
    {
        await Task.CompletedTask;
        return new List<TimetableDto>();
    }

    public async Task ChangeRoom(int roomId)
    {
        await Task.CompletedTask;
    }

    public async Task SeedDbForTimetable()
    {
        var lecturersTask = _umatApiService.GetLecturers();
        var groupsTask = _umatApiService.GetClasses();
        var coursesTask = _umatApiService.GetCourses();

        try
        {
			await Task.WhenAll(lecturersTask, groupsTask, coursesTask);
		}
		catch (Exception e)
        {
            _logger.LogError("Error while pulling data from UMaT API. Message: {Message}", e.Message);
            return;
        }
        
        var lecturers = lecturersTask.Result;
        var groups = groupsTask.Result;
        var courses = coursesTask.Result;

        await AddLecturers(lecturers);
        await AddGroups(groups);
        await AddSubClassGroups();
        await AddCoursesFromUmatDb(courses);
        await AddLectures();
    }

    private async Task AddLecturers(List<Staff>? lecturers)
    {
        var addedStaff = new List<Staff>();
        if (lecturers is not null)
        {
            foreach (var lecturer in lecturers)
            {
                var lecturerExists = addedStaff.Any(x => x.Id == lecturer.Id);
                if (lecturerExists) continue;
                await _lecturerRepository.AddAsync(Lecturer.Create(lecturer.Id, lecturer.Party?.Name?.FullName), false);
                addedStaff.Add(lecturer);
            }
        }

        await _lecturerRepository.SaveChanges();
    }

    private async Task AddGroups(List<Group>? groups)
    {
        var addedGroups = new List<Group>();
        if (groups is not null)
        {
            foreach (var group in groups)
            {
                var groupExists = addedGroups.Any(x => x.name == group.name);
                if (groupExists) continue;
                await _classGroupRepository.AddAsync(ClassGroup.Create(group.id, group.size, group.name), false);
                addedGroups.Add(group);
            }
        }

        await _classGroupRepository.SaveChanges();
    }

    private async Task AddSubClassGroups()
    {
        var groups = await _classGroupRepository.GetAll();
        foreach (var group in groups)
        {
            var size = group.Size;
            var count = 1;
            while (size > 0)
            {
                var number = group.Size - (60 * count) > 0 ? 60 : size;
                await _subClassGroupRepository.AddAsync(SubClassGroup.Create(group.Id, number,
                    $"{group.Name}{GetSubClassSuffix(count)}"), false);
                count += 1;
                size -= number;
            }
        }

        await _subClassGroupRepository.SaveChanges();
    }

    private static string GetSubClassSuffix(int count)
    {
        return count switch
        {
            1 => "A",
            2 => "B",
            3 => "C",
            4 => "D",
            5 => "E",
            6 => "F",
            7 => "G",
            8 => "H",
            9 => "I",
            10 => "J",
            11 => "K",
            12 => "L",
            13 => "M",
            14 => "N",
            15 => "O",
            16 => "P",
            17 => "Q",
            18 => "R",
            _ => ""
        };
    }

    private async Task AddCoursesFromUmatDb(List<CourseResponse>? courses)
    {
        if (courses is not null)
        {
            foreach (var course in courses)
            {
                var c = IncomingCourse.Create(course.Name ?? "", course.Credit, course.YearGroup, course.Id);
                c.ForAcademicPeriod(new AcademicPeriodResponse
                    {
                        AcademicYear = course.AcademicPeriod.AcademicYear,
                        LowerYear = course.AcademicPeriod.LowerYear,
                        Semester = course.AcademicPeriod.Semester,
                        UpperYear = course.AcademicPeriod.UpperYear
                    })
                    .HasCode(course.Code)
                    .ForYear(course.Year)
                    .HasGroup(course.CourseGroup)
                    .HasCategory(course.CourseCategory)
                    .HasType(course.CourseType)
                    .HasCourseId(course.CourseId)
                    .ForProgramme(course.Programme?.Id, course.Programme?.Code)
                    .HasExaminers(course.FirstExaminerStaff?.Id, course.SecondExaminerStaff?.Id)
                    .WithHours(null, null);
                await _courseRepository.AddAsync(c, false);
            }
        }

        await _courseRepository.SaveChanges();
    }

    private async Task AddLectures()
    {
        var savedCourses = await _courseRepository.GetAll();
        var classGroups = await _subClassGroupRepository.GetAll();
        foreach (var course in savedCourses)
        {
            if (course.Code!.StartsWith("EM 411") || course.Code!.StartsWith("EM 413")) continue;
            if (course.FirstExaminerStaffId is null) return;
            var courseNumber = course.Code!.Split(" ")[1];
            var professor = course.FirstExaminerStaffId;

            var groups = classGroups?.Where(x => course.Code!.StartsWith(x.Name[..^1])).ToList();
            if (groups is null) continue;
            
            foreach (var subClassGroup in groups)
            {
                if(course.TeachingHours != 0)
                {
					var teachingLecture = Lecture.Create(professor ?? 0, course.UmatId ?? 0, course.TeachingHours, courseNumber, false);
					teachingLecture.AddGroup(subClassGroup);
					await _lectureRepository.AddAsync(teachingLecture, false);
				}
                
                if(course.PracticalHours != 0)
                {
					var practicalLecture = Lecture.Create(professor ?? 0, course.UmatId ?? 0, course.PracticalHours, courseNumber, true);
					practicalLecture.AddGroup(subClassGroup);
					await _lectureRepository.AddAsync(practicalLecture, false);
				}
            }
        }

        await _lectureRepository.SaveChanges();
    }
}

public record TimetableDto(int CourseId, int RoomId, int Day, int Time, ClassRoom? Room);

public record Group(int id, string name, int size);