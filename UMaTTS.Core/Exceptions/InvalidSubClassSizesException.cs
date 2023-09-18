namespace UMaTLMS.Core.Exceptions;

public class InvalidSubClassSizesException : Exception
{
    public InvalidSubClassSizesException(string message = "The sizes for the subclasses do not match the group size") : base(message)
    {

    }
}
