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

    public LectureSchedule HasFirstLecture(Lecture lecture)
    {
        FirstLecture = lecture;
        FirstLectureId = lecture.Id;
        return this;
    }
    
    public LectureSchedule HasSecondLecture(Lecture lecture)
    {
        SecondLecture = lecture;
        SecondLectureId = lecture.Id;
        return this;
    }

    public LectureSchedule Reset()
    {
        FirstLectureId = null;
        FirstLecture = null;
        SecondLectureId = null;
        SecondLecture = null;
        return this;
    }
}

