using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using UMaTLMS.Core.Helpers;

namespace UMaTLMS.Core.Entities;

public class ExamsSchedule : Entity
{
    private ExamsSchedule() { }
    private ExamsSchedule(string courseCode)
    {
        SerializedCourseCodes = JsonSerializer.Serialize(new List<string> {courseCode});
    }
    
    public ExamPeriod? ExamPeriod { get; private set; }
    public DateTime DateOfExam { get; private set; }
    public int? RoomId { get; private set; }
    public string SerializedCourseCodes { get; set; }
    public string CourseNo => CourseCodes?[0].Split(AppHelpers.WhiteSpace)[1] ?? string.Empty;
    public int? ExaminerId { get; private set; }
    public string? Examiner { get; private set; }
    public string? CourseName { get; private set; }
    public ClassRoom? Room { get; private set; }
    
    private List<SubClassGroup> _subClassGroups = new();
    public IReadOnlyList<SubClassGroup> SubClassGroups => _subClassGroups.AsReadOnly();
    private List<Lecturer> _invigilators = new();
    public IReadOnlyList<Lecturer> Invigilators => _invigilators.AsReadOnly();
    
    [NotMapped]
    public List<string>? CourseCodes => JsonSerializer.Deserialize<List<string>>(SerializedCourseCodes);

    public static ExamsSchedule Create(string courseCode) =>
        new(courseCode);

    public ExamsSchedule AddGroup(SubClassGroup? group, List<string>? courseCodes = null)
    {
        if (group is null) return this;
        if (courseCodes is not null)
        {
            var codes = JsonSerializer.Deserialize<List<string>>(SerializedCourseCodes);
            codes?.AddRange(courseCodes);
            SerializedCourseCodes = JsonSerializer.Serialize(codes);
        }
        _subClassGroups.Add(group);
        return this;
    }

    public ExamsSchedule OnPeriod(ExamPeriod? period)
    {
        ExamPeriod = period;
        return this;
    }

    public ExamsSchedule ToBeWrittenInRoom(int? roomId)
    {
        RoomId = roomId;
        return this;
    }
    
    public ExamsSchedule ToBeWrittenOn(DateTime date)
    {
        DateOfExam = date;
        return this;
    }

    public ExamsSchedule ToBeInvigilatedBy(Lecturer? lecturer)
    {
        if (lecturer is null) return this;
        _invigilators.Add(lecturer);
        return this;
    }

    public ExamsSchedule HasInfo(int examinerId, string? examiner, string? courseName)
    {
        CourseName = courseName;
        Examiner = examiner;
        ExaminerId = examinerId;
        return this;
    }
}

public enum ExamPeriod
{
    Morning,
    Afternoon,
    Evening
}