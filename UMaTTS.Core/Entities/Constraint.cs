using UMaTLMS.Core.Enums;

namespace UMaTLMS.Core.Entities;
public class Constraint : Entity
{
    public Constraint(ConstraintType type, TimetableType timetableType, int? lecturerId)
    {
        Type = type;
        TimetableType = timetableType;
        LecturerId = lecturerId;
    }

    public ConstraintType Type { get; private set; }
    public int? LecturerId { get; private set; }
    public Lecturer? Lecturer { get; private set; }
    public string Value { get; private set; }
    public TimetableType TimetableType { get; private set; }

    public static Constraint Create(ConstraintType type, TimetableType timetableType, int? lecturerId = null) 
        => new (type, timetableType, lecturerId);

    public Constraint WithValue(string value)
    {
        Value = value;
        return this;
    }
}

public enum ConstraintType
{
    MaxLecturesPerDayForLecturer,
    GeneralMaxLecturesPerDay
}
