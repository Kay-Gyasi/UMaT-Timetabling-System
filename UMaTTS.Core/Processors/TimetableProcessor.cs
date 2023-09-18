using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using UMaTLMS.Core.Contracts;
using UMaTLMS.Core.Entities;
using UMaTLMS.Core.Helpers;
using UMaTLMS.Core.Services;
using UMaTLMS.SharedKernel.Helpers;

namespace UMaTLMS.Core.Processors;

[Processor]
public class TimetableProcessor
{
    private const string _timetableFile = "Timetable:File";
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
    private readonly IPreferenceRepository _preferenceRepository;
    private readonly IExcelReader _excelReader;
    private readonly IConfiguration _configuration;
    private readonly IConstraintRepository _constraintRepository;
    private readonly IAdminSettingsRepository _adminSettingsRepository;
    private readonly ClassProcessor _classProcessor;

    public TimetableProcessor(ILogger<TimetableProcessor> logger, ILectureScheduleRepository timetableRepository, 
        ILectureRepository lectureRepository, ILecturerRepository lecturerRepository, IClassGroupRepository classGroupRepository, 
        ICourseRepository courseRepository, ISubClassGroupRepository subClassGroupRepository, IUMaTApiService umatApiService,
        IOnlineLectureScheduleRepository onlineLectureScheduleRepository, IRoomRepository roomRepository,
        IPreferenceRepository preferenceRepository, IExcelReader excelReader, IConfiguration configuration, 
        IConstraintRepository constraintRepository, IAdminSettingsRepository adminSettingsRepository, ClassProcessor classProcessor)
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
        _preferenceRepository = preferenceRepository;
        _excelReader = excelReader;
        _configuration = configuration;
        _constraintRepository = constraintRepository;
        _adminSettingsRepository = adminSettingsRepository;
        _classProcessor = classProcessor;
    }

    public async Task<OneOf<bool, Exception>> Generate()
    {
        var fileName = _configuration[_timetableFile] ?? string.Empty;
        if (File.Exists(fileName)) return new TimetableGeneratedException();

        var lectures = await _lectureRepository.GetAllAsync();
        var schedules = await _lectureScheduleRepository.GetAllAsync();
        var preferences = await _preferenceRepository.GetAllAsync(x => x.TimetableType == Enums.TimetableType.Lectures);
        var constraints = await _constraintRepository.GetAllAsync(x => x.TimetableType == Enums.TimetableType.Lectures);

        var onlineSchedules = await _onlineLectureScheduleRepository.GetAllAsync();
        var rooms = await _roomRepository.GetAllAsync();

        if (!lectures.Any() || !schedules.Any() || !onlineSchedules.Any() || !rooms.Any())
        {
            return new LecturesNotGeneratedException();
        }

        try
        {
            bool allLecturesHaveBeenScheduled = false;
            List<LectureSchedule> GeneralSchedules = new(); 
            List<OnlineLectureSchedule> OnlineSchedules = new();
            while (!allLecturesHaveBeenScheduled)
            {
                (GeneralSchedules, OnlineSchedules) = TimetableGenerator.Generate(schedules, onlineSchedules, lectures, preferences, constraints);
                _logger.LogInformation("Done generating lecture schedules");

                var numOfLecturesNotScheduled = GetNumberOfLecturesNotScheduled(lectures, GeneralSchedules, OnlineSchedules);
                if (numOfLecturesNotScheduled == 0) allLecturesHaveBeenScheduled = true;
            }

            await SaveSchedulesToDatabase(schedules, OnlineSchedules);
            await TimetableGenerator.GetAsync(_excelReader, GeneralSchedules, OnlineSchedules, rooms, fileName);
            _logger.LogInformation("Done building timetable!");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while generating timetable. Message: {Message}", ex.Message);
            return ex;
        }
    }

    public async Task<OneOf<bool, Exception>> GenerateLectures()
    {
        try
        {
            await InitializeLectureSchedule();
            var classGroupsIsSeeded = await _subClassGroupRepository.IsSeeded();
            if (!classGroupsIsSeeded) await _classProcessor.AddSubClassGroups();
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
        var groupsTask = _umatApiService.GetClasses();
        var coursesTask = _umatApiService.GetCourses();

        try
        {
            await Task.WhenAll(groupsTask, coursesTask);

            await AddGroups(groupsTask.Result);
            await AddCoursesFromUmatDb(coursesTask.Result);

            return await _courseRepository.SaveChanges();
        }
        catch (Exception e)
        {
            _logger.LogError("Error while pulling data from UMaT API. Exception: {Exception}", e);
            return e;
        }
    }

    private async Task InitializeLectureSchedule()
    {
        var rooms = await _roomRepository.GetAllAsync();
        if (!rooms.Any()) throw new SystemNotInitializedException();

        var timeSlotSetting = await _adminSettingsRepository.GetAsync(x => x.Key == AdminConfigurationKeys.LectureTimeSlots);
        if (timeSlotSetting is null) throw new SystemNotInitializedException("Timeslots not set");
        var timeSlots = JsonSerializer.Deserialize<List<string>>(timeSlotSetting.Value) ?? new List<string>();
        
        var dayOfWeekSetting = await _adminSettingsRepository.GetAsync(x => x.Key == AdminConfigurationKeys.DaysOfWeek);
        if (dayOfWeekSetting is null) throw new SystemNotInitializedException("Days of week not set");
        var daysOfWeek = JsonSerializer.Deserialize<List<string>>(dayOfWeekSetting.Value) ?? new List<string>();

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
            foreach (var dayOfWeek in daysOfWeek)
            {
                foreach (var room in rooms)
                {
                    foreach (var timeSlot in timeSlots)
                    {
                        var isDow = Enum.TryParse(typeof(DayOfWeek), dayOfWeek, ignoreCase: true, out var output);
                        if (!isDow) continue;

                        var dow = output as DayOfWeek?;
                        var schedule = LectureSchedule.Create(dow, timeSlot, room.Id);
                        if (dow == DayOfWeek.Friday && timeSlot is "4:30pm" or "6:30pm") continue;
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
            foreach (var dayOfWeek in daysOfWeek)
            {
                foreach (var timeSlot in timeSlots)
                {
                    var isDow = Enum.TryParse(typeof(DayOfWeek), dayOfWeek, ignoreCase: true, out var output);
                    if (!isDow) continue;

                    var dow = output as DayOfWeek?;
                    var online = OnlineLectureSchedule.Create(dow, timeSlot);
                    if (dow == DayOfWeek.Friday && timeSlot is "4:30pm" or "6:30pm") continue;
                    await _onlineLectureScheduleRepository.AddAsync(online, saveChanges: false);
                }
            }
        }

        await _lectureScheduleRepository.SaveChanges();
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

    private async Task AddCoursesFromUmatDb(List<CourseResponse>? courses)
    {
        if (courses == null) return;
        var coursesInDb = await _courseRepository.GetAllAsync();
        var lecturersInDb = await _lecturerRepository.GetAllAsync();

        foreach (var course in courses)
        {
            if (coursesInDb.Any(x => x.YearGroup == course.YearGroup && x.UmatId == course.Id)) 
                continue;

            await AddCourseExaminer(course.FirstExaminerStaff, lecturersInDb);
            await AddCourseExaminer(course.SecondExaminerStaff, lecturersInDb);

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
            CreateLectures(lecturers, classGroups, lectures, course);
        }

        foreach (var lecture in lectures)
        {
            if (lecture.Duration == 0) continue;
            await _lectureRepository.AddAsync(lecture, saveChanges: false).ConfigureAwait(false);
        }

        await _lectureRepository.SaveChanges();
    }

    private void CreateLectures(List<Lecturer> lecturers, List<ClassGroup> classGroups, List<Lecture> lectures, IncomingCourse course)
    {
        if (course.Code!.StartsWith("EM 411") || course.Code!.StartsWith("EM 413")) return;
        if (course.FirstExaminerStaffId is null) return;
        var courseCode = course.Code?.Trim().Split(AppHelpers.WhiteSpace)[1];
        var lecturer = lecturers.FirstOrDefault(x => x.UmatId == course.FirstExaminerStaffId);
        if (lecturer is null) return;

        var isCreated = AddGroupToLectureIfAlreadyCreated(lectures, classGroups, lecturer.Id, course, courseCode);
        if (isCreated) return;

        var subs = classGroups.FirstOrDefault(x => x.Name.StartsWith(course.ProgrammeCode!) 
                                                    && x.Year == course.Year)?
                                                    .SubClassGroups;

        var teaching = Lecture.Create(lecturer.Id, course.Id, course.TeachingHours)
                                .ForCourse(course)
                                .AddGroups(subs);

        var practical = Lecture.Create(lecturer.Id, course.Id, course.PracticalHours, true)
                                .ForCourse(course)
                                .AddGroups(subs);

        lectures.AddRange(new List<Lecture> { teaching, practical });
    }

    private bool AddGroupToLectureIfAlreadyCreated(List<Lecture> lectures, List<ClassGroup> classGroups, int lecturerId,
        IncomingCourse course, string? courseCode)
    {
        var insertedLectures = lectures.Where(x =>
                x.Course?.Code?.Split(AppHelpers.WhiteSpace)[1] == courseCode
                && x.LecturerId == lecturerId).ToList();

        if (!insertedLectures.Any()) return false;

        foreach (var lecture in insertedLectures)
        {
            var subs2 = classGroups.FirstOrDefault(x =>
                x.Name.StartsWith(course.ProgrammeCode!)
                    && x.Year == course.Year)?
                        .SubClassGroups;
            lecture.AddGroups(subs2);
        }
        return true;
    }

    private async Task AddCourseExaminer(Staff? staff, List<Lecturer> lecturersInDb)
    {
        if (staff is null) return;
        var lecturerExists = lecturersInDb.Any(x => x.UmatId == staff?.Id);
        if (!lecturerExists)
        {
            var title = staff?.Party?.Name?.FullNamev2?.Split("(")[1][..^1];
            await _lecturerRepository.AddAsync(
                Lecturer.Create(staff!.Id, staff?.Party?.Name?.FullName, staff?.Party?.Name?.FullNamev2),
                                    saveChanges: false);
        }
    }

    private async Task SaveSchedulesToDatabase(List<LectureSchedule> schedules, List<OnlineLectureSchedule> onlineSchedules)
    {
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
    }

    private static int GetNumberOfLecturesNotScheduled(List<Lecture> lecturesInDb, List<LectureSchedule> lectureSchedules,
        List<OnlineLectureSchedule> onlineLectureSchedules)
    {
        int result = 0;
        List<Lecture> lecturesNotScheduled = new List<Lecture>();

        foreach (var lecture in lecturesInDb.Where(x => x.Course!.IsToHaveWeeklyLectureSchedule && !x.IsVLE))
        {
            var lectureIsScheduled = lectureSchedules.Any(x => x.FirstLectureId == lecture.Id || x.SecondLectureId == lecture.Id);
            if (!lectureIsScheduled)
            {
                lecturesNotScheduled.Add(lecture);
                result++;
            }
        }
        
        foreach (var lecture in lecturesInDb.Where(x => x.Course!.IsToHaveWeeklyLectureSchedule && x.IsVLE))
        {
            var onlineLectureIsScheduled = onlineLectureSchedules.Any(x => x.Lectures.Contains(lecture));
            if (!onlineLectureIsScheduled)
            {
                lecturesNotScheduled.Add(lecture);
                result++;
            }
        }

        return result;
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