namespace UMaTLMS.Core.Exceptions;

public class DataNotSyncedWithUmatException : Exception
{
    public DataNotSyncedWithUmatException(string message = "Data not pulled from UMaT systems") : base(message)
    {

    }
}