namespace UMaTLMS.Core.Exceptions;

public class InvalidIdException : Exception
{
    public InvalidIdException()
    {
    }

    public InvalidIdException(string message) : base(message)
    {

    }
}

public class InvalidLoginException : Exception
{
    public InvalidLoginException(string message = "Invalid login details") : base(message)
    {

    }
}

public class EntityExistsException : Exception
{
    public EntityExistsException()
    {
    }

    public EntityExistsException(string message) : base(message)
    {

    }
}

public class RepositoryNotFoundException : Exception
{
    public RepositoryNotFoundException()
    {
    }

    public RepositoryNotFoundException(string message) : base(message)
    {

    }
}
