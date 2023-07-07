namespace UMaTLMS.Core.Exceptions;

public class LecturesNotScheduledException : Exception
{
    public LecturesNotScheduledException(string message = "Some lectures have not been scheduled") : base(message)
    {

    }
}
