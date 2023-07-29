using UMaTLMS.Core.Enums;

namespace UMaTLMS.Core.Entities;

public class Preference : Entity
{
    private Preference(PreferenceType type, TimetableType timetableType, int? lecturerId, int? courseId)
    {
        Type = type;
        TimetableType = timetableType;
        LecturerId = lecturerId;
        CourseId = courseId;
    }

    public PreferenceType Type { get; private set; }

    /// <summary>
    /// Serialized object for preference. Deserialize to specific type upon usage
    /// </summary>
    public string Value { get; private set; }
    public TimetableType TimetableType { get; private set; }
    public int? LecturerId { get; private set; }
    public Lecturer? Lecturer { get; private set; }
    public int? CourseId { get; private set; }
    public IncomingCourse? Course { get; private set; }

    public static Preference Create(PreferenceType type, TimetableType timetableType, int? lecturerId = null, int? courseId = null)
        => new(type, timetableType, lecturerId, courseId);

    public Preference WithValue(string value)
    {
        Value = value;
        return this;
    }
}

public enum PreferenceType
{
    DayNotAvailable,
    TimeNotAvailable,
    PreferredDayOfWeek
}