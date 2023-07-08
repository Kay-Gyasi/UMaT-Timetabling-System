namespace UMaTLMS.Core.Exceptions;

public class InvalidIdException : Exception
{
    public InvalidIdException(string message = "The id provided is invalid") : base(message)
    {

    }
}
