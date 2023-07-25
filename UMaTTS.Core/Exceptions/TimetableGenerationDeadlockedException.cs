namespace UMaTLMS.Core.Exceptions;

public class TimetableGenerationDeadlockedException : Exception
{
    public TimetableGenerationDeadlockedException(string message = "The generator is in a deadlock. Please try again") : base(message)
    {

    }
}
