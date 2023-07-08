namespace UMaTLMS.Core.Exceptions;

public class LecturesNotGeneratedException : Exception
{
    public LecturesNotGeneratedException(string message = "Lectures have not been generated") : base(message)
    {

    }
}
