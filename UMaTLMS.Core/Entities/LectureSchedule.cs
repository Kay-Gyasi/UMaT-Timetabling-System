namespace UMaTLMS.Core.Entities;

public class LectureSchedule : Entity
{
    private LectureSchedule(DayOfWeek? dayOfWeek, string timePeriod, int roomId)
    {
        DayOfWeek = dayOfWeek;
        TimePeriod = timePeriod;
        RoomId = roomId;
    }

    public DayOfWeek? DayOfWeek { get; private set; }
    public string TimePeriod { get; private set; }
    public int RoomId { get; private set; }
    public int? FirstLectureId { get; private set; }
    public int? SecondLectureId { get; private set; }
    public ClassRoom Room { get; private set; }
    public Lecture? FirstLecture { get; private set; }
    public Lecture? SecondLecture { get; private set; }

    public static LectureSchedule Create(DayOfWeek? dayOfWeek, string timePeriod, int roomId)
        => new(dayOfWeek, timePeriod, roomId);

    public LectureSchedule HasLecture(int? firstLectureId, int? secondLectureId)
    {
        if (firstLectureId is not null) FirstLectureId = firstLectureId;
        if (secondLectureId is not null) SecondLectureId = secondLectureId;
        return this;
    }

    public LectureSchedule Reset()
    {
        FirstLectureId = null;
        SecondLectureId = null;
        return this;
    }
}

