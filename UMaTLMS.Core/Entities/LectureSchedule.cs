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
    public int? LectureId { get; private set; }
    public ClassRoom Room { get; private set; }
    public Lecture? Lecture { get; private set; }

    public static LectureSchedule Create(DayOfWeek? dayOfWeek, string timePeriod, int roomId)
        => new(dayOfWeek, timePeriod, roomId);

    public LectureSchedule HasLecture(int lectureId)
    {
        LectureId = lectureId;
        return this;
    }
}

