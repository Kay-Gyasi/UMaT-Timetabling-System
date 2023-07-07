namespace UMaTLMS.Core.Exceptions;

public class ExamNotAssignedRoomException : Exception
{
    public ExamNotAssignedRoomException(string message = "No room was found for exam") : base(message)
    {

    }
}