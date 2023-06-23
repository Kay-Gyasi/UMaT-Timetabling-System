namespace UMaTLMS.Core.Entities;

public class OnlineLectureSchedule : Entity
{
    private OnlineLectureSchedule(DayOfWeek? dayOfWeek, string timePeriod)
    {
        DayOfWeek = dayOfWeek;
        TimePeriod = timePeriod;
    }

    public DayOfWeek? DayOfWeek { get; private set; }
    public string TimePeriod { get; private set; }
    private List<Lecture> _lectures = new();
    public IReadOnlyList<Lecture> Lectures => _lectures.AsReadOnly();

    public static OnlineLectureSchedule Create(DayOfWeek? dayOfWeek, string timePeriod)
        => new(dayOfWeek, timePeriod);

    public OnlineLectureSchedule AddLectures(IEnumerable<Lecture> lectures)
    {
        _lectures.AddRange(lectures);
        return this;
    }
    
    public OnlineLectureSchedule AddLecture(Lecture lecture)
    {
        _lectures.Add(lecture);
        return this;
    }

    public OnlineLectureSchedule Reset()
    {
        _lectures.Clear();
        return this;
    }
}

