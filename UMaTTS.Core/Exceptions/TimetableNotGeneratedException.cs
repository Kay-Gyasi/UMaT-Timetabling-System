namespace UMaTLMS.Core.Exceptions;

public class TimetableGeneratedException : Exception
{
    public TimetableGeneratedException(string message = "Timetable has been generated") : base(message)
    {

    }
}
