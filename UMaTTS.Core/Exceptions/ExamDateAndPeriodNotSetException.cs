namespace UMaTLMS.Core.Exceptions
{
    public class ExamDateAndPeriodNotSetException : Exception
    {
        public ExamDateAndPeriodNotSetException(string message = "No date and period has been set for exam") : base(message)
        {

        }
    }
}