namespace UMaTLMS.Core.Exceptions;

public class SystemNotInitializedException : Exception
{
    public SystemNotInitializedException(string message = "System not initialized") : base(message)
    {

    }
}
