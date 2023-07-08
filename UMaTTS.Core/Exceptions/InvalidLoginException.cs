namespace UMaTLMS.Core.Exceptions;

public class InvalidLoginException : Exception
{
    public InvalidLoginException(string message = "Invalid login details") : base(message)
    {

    }
}
