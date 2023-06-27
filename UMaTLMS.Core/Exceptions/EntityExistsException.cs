namespace UMaTLMS.Core.Exceptions;

public class EntityExistsException : Exception
{
    public EntityExistsException(string message = "Entity already exists in database") : base(message)
    {

    }
}
