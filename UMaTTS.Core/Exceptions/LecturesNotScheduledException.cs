namespace UMaTLMS.Core.Exceptions;

public class LecturesNotScheduledException : Exception
{
    public LecturesNotScheduledException(string message = "Some lectures have not been scheduled") : base(message)
    {

    }
}

public class ExamNotScheduledException : Exception
{
    public ExamNotScheduledException(string message = "Some exams have not been scheduled") : base(message)
    {

    }
}
