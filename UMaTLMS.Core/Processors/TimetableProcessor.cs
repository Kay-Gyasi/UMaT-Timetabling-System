using System.Text.Json;
using LinqKit;
using UMaTLMS.Core.Contracts;
using UMaTLMS.Core.Services;
using UMaTLMS.SharedKernel.Helpers;

namespace UMaTLMS.Core.Processors;

[Processor]
public class TimetableProcessor
{
    private readonly ILogger<TimetableProcessor> _logger;
    private readonly RoomProcessor _roomProcessor;
    private readonly ILectureScheduleRepository _lectureScheduleRepository;
    private readonly ILectureRepository _lectureRepository;
    private readonly ILecturerRepository _lecturerRepository;
    private readonly IClassGroupRepository _classGroupRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly ISubClassGroupRepository _subClassGroupRepository;
    private readonly IUMaTApiService _umatApiService;

    public TimetableProcessor(ILogger<TimetableProcessor> logger, RoomProcessor roomProcessor,
        ILectureScheduleRepository timetableRepository, ILectureRepository lectureRepository, 
        ILecturerRepository lecturerRepository, IClassGroupRepository classGroupRepository, 
        ICourseRepository courseRepository, ISubClassGroupRepository subClassGroupRepository, IUMaTApiService uMaTApiService)
    {
        _logger = logger;
        _roomProcessor = roomProcessor;
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
        // TODO:: Go to where lectures are created and use duration or credit hours to properly create (teaching & practical sessions)

        Shuffle(schedules);
        foreach (var lecture in lectures)
        {
            var builder = PredicateBuilder.New<LectureSchedule>(x => true);
            
            for (var i = 0; i < 5; i++)
            {
                var numOfLecturesForLecturer =
                    await _lectureScheduleRepository.GetNumberOfLecturesForLecturerInADay(lecture.LecturerId, i);
                if (numOfLecturesForLecturer < 4) continue;

                var i1 = i;
                builder.And(x => x.DayOfWeek != AppHelper.GetDayOfWeek(i1));
            }
            
            var eligibleSchedules = schedules.Where(builder);
            var room = eligibleSchedules.FirstOrDefault(x => x.LectureId == null);
            room?.HasLecture(lecture.Id);
        }

        foreach (var schedule in schedules)
        {
            await _lectureScheduleRepository.UpdateAsync(schedule).ConfigureAwait(false);
        }
    }

    public async Task<IEnumerable<TimetableDto>> GetClassSchedule(int classId)
    {
        await Task.CompletedTask;
        return new List<TimetableDto>();
    }

    public async Task SeedDbForTimetable()
    {
        var lecturers = await _umatApiService.GetLecturers();
        var groups = await _umatApiService.GetClasses();
        var courses = await _umatApiService.GetCourses();

        await AddLecturers(lecturers);
        await AddGroups(groups);
        await AddSubClassGroups();
        await AddCoursesFromUmatDb(courses);
        
        //TODO:: Get actual duration of classes from course distribution
        //TODO:: Check how to indicate if course is a lab course or not
        //TODO:: Check values for courses types, categories from lookup
        await AddLectures();
    }

    private async Task AddLecturers(List<Staff>? lecturers)
    {
        if (lecturers is not null)
        {
            foreach (var lecturer in lecturers)
            {
                var lecturerExists = await _lecturerRepository.Exists(lecturer.Id);
                if (lecturerExists) continue;
                await _lecturerRepository.AddAsync(Lecturer.Create(lecturer.Id, lecturer.Party?.Name?.FullName));
            }
        }
    }

    private async Task AddGroups(List<Group>? groups)
    {
        if (groups is not null)
        {
            foreach (var group in groups)
            {
                var groupExists = await _classGroupRepository.Exists(group.name);
                if (groupExists) continue;
                await _classGroupRepository.AddAsync(ClassGroup.Create(group.id, group.size, group.name));
            }
        }
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
                    $"{group.Name}{GetSubClassSuffix(count)}"));
                count += 1;
                size -= number;
            }
        }
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
                    .HasExaminers(course.FirstExaminerStaff?.Id, course.SecondExaminerStaff?.Id);
                await _courseRepository.AddAsync(c);
            }
        }
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
                var lecture = Lecture.Create(professor ?? 0, 
                    course.UmatId ?? 0, 2, courseNumber, false);
                lecture.AddGroup(subClassGroup);
                await _lectureRepository.AddAsync(lecture);
            }
        }
    }

    private static void Shuffle<T>(List<T> list)
    {
        var random = new Random();

        for (var i = list.Count - 1; i > 0; i--)
        {
            var j = random.Next(i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}

public record TimetableDto(int CourseId, int RoomId, int Day, int Time, ClassRoom? Room);

public record Group(int id, string name, int size);