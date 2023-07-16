using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;
using UMaTLMS.Core.Contracts;
using UMaTLMS.Core.Helpers;
using UMaTLMS.Core.Services;
using UMaTLMS.SharedKernel.Helpers;

namespace UMaTLMS.Core.Processors;

[Processor]
public class TimetableProcessor
{
    private const string _timetableFile = "Timetable:File";
    private const string _timetableType = "Timetable:Type";
    private const string _timetableDownload = "Timetable:DownloadName";
    private readonly ILogger<TimetableProcessor> _logger;
    private readonly ILectureScheduleRepository _lectureScheduleRepository;
    private readonly ILectureRepository _lectureRepository;
    private readonly ILecturerRepository _lecturerRepository;
    private readonly IClassGroupRepository _classGroupRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly ISubClassGroupRepository _subClassGroupRepository;
    private readonly IUMaTApiService _umatApiService;
    private readonly IOnlineLectureScheduleRepository _onlineLectureScheduleRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IExcelReader _excelReader;
    private readonly IConfiguration _configuration;

    public TimetableProcessor(ILogger<TimetableProcessor> logger, ILectureScheduleRepository timetableRepository, 
        ILectureRepository lectureRepository, ILecturerRepository lecturerRepository, IClassGroupRepository classGroupRepository, 
        ICourseRepository courseRepository, ISubClassGroupRepository subClassGroupRepository, IUMaTApiService umatApiService,
        IOnlineLectureScheduleRepository onlineLectureScheduleRepository, IRoomRepository roomRepository,
        IExcelReader excelReader, IConfiguration configuration)
    {
        _logger = logger;
        _lectureScheduleRepository = timetableRepository;
        _lectureRepository = lectureRepository;
        _lecturerRepository = lecturerRepository;
        _classGroupRepository = classGroupRepository;
        _courseRepository = courseRepository;
        _subClassGroupRepository = subClassGroupRepository;
        _umatApiService = umatApiService;
        _onlineLectureScheduleRepository = onlineLectureScheduleRepository;
        _roomRepository = roomRepository;
        _excelReader = excelReader;
        _configuration = configuration;
    }

    public async Task<OneOf<bool, Exception>> Generate()
    {
        var fileName = _configuration[_timetableFile] ?? string.Empty;
        if (File.Exists(fileName)) return new TimetableGeneratedException();

        var lectures = await _lectureRepository.GetAllAsync();
        var schedules = await _lectureScheduleRepository.GetAllAsync();

        var onlineSchedules = await _onlineLectureScheduleRepository.GetAllAsync();
        var rooms = await _roomRepository.GetAllAsync();

        if (!lectures.Any() || !schedules.Any() || !onlineSchedules.Any() || !rooms.Any())
        {
            return new LecturesNotGeneratedException();
        }

        var result = TimetableGenerator.Generate(schedules, onlineSchedules, lectures);
        schedules = result.GeneralSchedules;
        onlineSchedules = result.OnlineSchedules;

        var lecturesNotScheduledCount = GetCountOfLecturesNotScheduled(lectures, schedules);
        if (lecturesNotScheduledCount > 0)
        {
            return new LecturesNotScheduledException();
        }

        foreach (var schedule in schedules)
        {
            await _lectureScheduleRepository.UpdateAsync(schedule, false)
                .ConfigureAwait(false);
        }
        
        foreach (var schedule in onlineSchedules)
        {
            await _onlineLectureScheduleRepository.UpdateAsync(schedule, false)
                .ConfigureAwait(false);
        }

        await _lectureScheduleRepository.SaveChanges();
        await TimetableGenerator.GetAsync(_excelReader, schedules, onlineSchedules, rooms, fileName);
        return true;
    }

    public async Task<OneOf<bool, Exception>> GenerateLectures()
    {
        try
        {
            await InitializeLectureSchedule();
            await AddSubClassGroups();
            await AddLectures();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while generating lectures. Error: {Message}", ex.Message);
            return ex;
        }
    }

    public async Task<OneOf<bool, Exception>> SyncWithUMaT()
    {
        var lecturersTask = _umatApiService.GetLecturers();
        var groupsTask = _umatApiService.GetClasses();
        var coursesTask = _umatApiService.GetCourses();

        try
        {
            // TODO:: remove reading from files when going to production
            await Task.WhenAll(lecturersTask, groupsTask, coursesTask);

            var lecturers = lecturersTask.Result;
            var groups = groupsTask.Result;
            var courses = coursesTask.Result;

            await AddLecturers(lecturers);
            await AddGroups(groups);
            await AddCoursesFromUmatDb(courses);

            return await _lecturerRepository.SaveChanges();
        }
        catch (Exception e)
        {
            _logger.LogError("Error while pulling data from UMaT API. Message: {Message}", e.Message);
            return e;
        }
    }

    private async Task InitializeLectureSchedule()
    {
        var rooms = await _roomRepository.GetAllAsync();
        if (!rooms.Any()) throw new SystemNotInitializedException();

        var timeSlots = GetTimeSLots().ToList();

        var schedules = await _lectureScheduleRepository.GetAllAsync();
        if (schedules.Any())
        {
            foreach (var schedule in schedules)
            {
                schedule.Reset();
            }
        }
        else
        {
            for (var i = 0; i < 5; i++)
            {
                foreach (var room in rooms)
                {
                    foreach (var timeSlot in timeSlots)
                    {
                        var schedule = LectureSchedule.Create(AppHelper.GetDayOfWeek(i), timeSlot, room.Id);
                        if (i == 4 && timeSlot is "4:30pm" or "6:30pm") continue;
                        await _lectureScheduleRepository.AddAsync(schedule, saveChanges: false);
                    }
                }
            }
        }

        var onlineSchedules = (await _onlineLectureScheduleRepository.GetAllAsync()).ToList();
        if (onlineSchedules.Any())
        {
            foreach (var schedule in onlineSchedules)
            {
                schedule.Reset();
            }
        }
        else
        {
            for (var i = 0; i < 5; i++)
            {
                foreach (var timeSlot in timeSlots)
                {
                    var online = OnlineLectureSchedule.Create(AppHelper.GetDayOfWeek(i), timeSlot);
                    if (i == 4 && timeSlot is "4:30pm" or "6:30pm") continue;
                    await _onlineLectureScheduleRepository.AddAsync(online, saveChanges: false);
                }
            }
        }

        await _lectureScheduleRepository.SaveChanges();
    }

    private async Task AddLecturers(List<Staff>? lecturers)
    {
        var lecturersInDb = await _lecturerRepository.GetAllAsync();
        if (lecturers is not null)
        {
            foreach (var lecturer in lecturers)
            {
                var lecturerExists = lecturersInDb.Any(x => x.Name == lecturer.Party?.Name?.FullName && x.UmatId == lecturer.Id);
                if (lecturerExists) continue;

                await _lecturerRepository.AddAsync(Lecturer.Create(lecturer.Id, lecturer.Party?.Name?.FullName), 
                    saveChanges: false);
            }
        }
    }

    private async Task AddGroups(List<Group>? groups)
    {
        var groupsInDb = await _classGroupRepository.GetAllAsync();
        groups = groups?.Distinct(new GroupComparer()).ToList();

        if (groups is not null)
        {
            foreach (var group in groups)
            {
                var groupExists = groupsInDb.FirstOrDefault(x => x.Name == group.Name);
                if (groupExists is not null)
                {
                    groupExists.HasSize(group.Size)
                        .HasNoOfSubClasses((group.Size / 80) + 1);
                    await _classGroupRepository.UpdateAsync(groupExists, saveChanges: false);
                    continue;
                }

                var entity = ClassGroup.Create(group.Id, group.Size, group.Name)
                    .HasNoOfSubClasses((group.Size / 80) + 1);
                await _classGroupRepository.AddAsync(entity, saveChanges: false);
            }
        }
    }

    private async Task AddSubClassGroups()
    {
        var groups = await _classGroupRepository.GetAllAsync() 
            ?? throw new DataNotSyncedWithUmatException();

        var subGroups = await _subClassGroupRepository.GetAllAsync();
        await _subClassGroupRepository.DeleteAllAsync(subGroups, saveChanges: false);

        foreach (var group in groups)
        {
            var sizes = group.Size.GetSubClassSizes(group.NumOfSubClasses);
            for (int i = 1; i <= group.NumOfSubClasses; i++)
            {
                var subName = group.NumOfSubClasses > 1 ? $"{group.Name}{AppHelpers.GetSubClassSuffix(i)}" : group.Name;
                await _subClassGroupRepository.AddAsync(SubClassGroup.Create(group.Id, sizes[i - 1],
                    subName), saveChanges: false);
            }
        }

        await _subClassGroupRepository.SaveChanges();
    }

    private async Task AddCoursesFromUmatDb(List<CourseResponse>? courses)
    {
        if (courses == null) return;
        var coursesInDb = await _courseRepository.GetAllAsync();
        if (coursesInDb.Any()) return;

        foreach (var course in courses)
        {
            var c = IncomingCourse.Create(course.Name?.Trim() ?? "", course.Credit, course.YearGroup, course.Id);
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
            await _courseRepository.AddAsync(c, saveChanges: false).ConfigureAwait(false);
        }
    }

    private async Task AddLectures()
    {
        var lecturableCourses = await _courseRepository.GetAllAsync(x => x.IsToHaveWeeklyLectureSchedule);
        var classGroups = await _classGroupRepository.GetAllAsync();
        var lecturers = await _lecturerRepository.GetAllAsync();

        if (!lecturableCourses.Any() || !classGroups.Any() || !lecturers.Any())
            throw new DataNotSyncedWithUmatException();

        var seededLectures = await _lectureRepository.GetAllAsync();
        if (seededLectures.Any()) await _lectureRepository.DeleteAllAsync(seededLectures, saveChanges: false);

        var lectures = new List<Lecture>();
        foreach (var course in lecturableCourses)
        {
            if (course.Code!.StartsWith("EM 411") || course.Code!.StartsWith("EM 413")) continue;
            if (course.FirstExaminerStaffId is null) continue;
            var courseCode = course.Code?.Trim().Split(AppHelpers.WhiteSpace)[1];

            var lecturerId = lecturers.FirstOrDefault(x => x.UmatId == course.FirstExaminerStaffId)?.Id;
            var insertedLectures = lectures.Where(x => 
                x.Course?.Code?.Split(AppHelpers.WhiteSpace)[1] == courseCode 
                && x.LecturerId == lecturerId).ToList();

            if (insertedLectures.Any()) 
            {
                foreach (var lecture in insertedLectures)
                {
                    var subs2 = classGroups.FirstOrDefault(x => 
                        x.Name.StartsWith(course.ProgrammeCode!) 
                            && x.Year == course.Year)?
                                .SubClassGroups;
                    lecture.AddGroups(subs2);
                }
                continue;
            }

            var lecturer = lecturers.FirstOrDefault(x => x.UmatId == course.FirstExaminerStaffId);
            if (lecturer is null) return;

            var subs = classGroups.FirstOrDefault(x => x.Name.StartsWith(course.ProgrammeCode!) && x.Year == course.Year)?
                    .SubClassGroups;

            var teaching = Lecture.Create(lecturer.Id, course.Id, course.TeachingHours)
                .ForCourse(course)
                .AddGroups(subs);
            
            var practical = Lecture.Create(lecturer.Id, course.Id, course.PracticalHours, true)
                .ForCourse(course)
                .AddGroups(subs);

            lectures.AddRange(new List<Lecture> { teaching, practical });
        }

        foreach (var lecture in lectures)
        {
            if (lecture.Duration == 0) continue;
            await _lectureRepository.AddAsync(lecture, saveChanges: false).ConfigureAwait(false);
        }

        await _lectureRepository.SaveChanges();
    }

    private static int GetCountOfLecturesNotScheduled(List<Lecture> lecturesInDb, List<LectureSchedule> lectureSchedules)
    {
        int result = 0;
        var lecturesScheduled = lectureSchedules.Select(x => x.Id).ToList();
        foreach (var lecture in lecturesInDb.Where(x => !x.IsVLE))
        {
            if (!lecturesScheduled.Any(x => x == lecture.Id))
            {
                result++;
            }
        }
        
        foreach (var lecture in lecturesInDb.Where(x => x.IsVLE))
        {
            if (lecture.OnlineLectureScheduleId == null)
            {
                result++;
            }
        }

        return result;
    }

    private static IEnumerable<string> GetTimeSLots()
    {
        return new List<string>()
        {
            "6am",
            "8am",
            "10am",
            "12:30pm",
            "2:30pm",
            "4:30pm"
        };
    }
}

public record TimetableDto(int CourseId, int RoomId, int Day, int Time, ClassRoom? Room);

public record Group(int Id, string Name, int Size);

public class GroupComparer : IEqualityComparer<Group>
{
    public bool Equals(Group? x, Group? y)
    {
        return x?.Name == y?.Name;
    }

    public int GetHashCode([DisallowNull] Group obj)
    {
        return obj.GetHashCode();
    }
}