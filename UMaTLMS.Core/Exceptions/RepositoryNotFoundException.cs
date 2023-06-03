namespace UMaTLMS.Core.Exceptions;

public class RepositoryNotFoundException : Exception
{
    public RepositoryNotFoundException()
    {
    }

    public RepositoryNotFoundException(string message) : base(message)
    {

    }
}
