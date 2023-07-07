namespace UMaTLMS.Core.Entities;

public class ExamsSchedule : Entity
{
    public ExamsSchedule(string courseCode, int subClassGroupId)
    {
        CourseCode = courseCode;
        SubClassGroupId = subClassGroupId;
    }
    
    public ExamPeriod? ExamPeriod { get; private set; }
    public DateTime DateOfExam { get; private set; }
    public int? RoomId { get; private set; }
    public int SubClassGroupId { get; private set; }
    public string CourseCode { get; private set; }
    public string CourseNo => CourseCode.Split(" ")[1];
    public int? InvigilatorId { get; private set; }
    public Lecturer? Invigilator { get; private set; }
    public int? ExaminerId { get; private set; }
    public Lecturer? Examiner { get; private set; }
    public ClassRoom? Room { get; private set; }
    

    public static ExamsSchedule Create(string courseCode, int subClassGroupId) =>
        new(courseCode, subClassGroupId);

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

    public ExamsSchedule ToBeInvigilatedBy(int lecturerId)
    {
        InvigilatorId = lecturerId;
        return this;
    }

    public ExamsSchedule ToBeExaminedBy(int examinerId)
    {
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