namespace UMaTLMS.Core.Contracts;

public class AcademicPeriodResponse
{
    public int LowerYear { get; set; }
    public int UpperYear { get; set; }
    public string AcademicYear { get; set; }
    public int Semester { get; set; }
}

public class AcademicPeriod
{
    public string Version { get; set; }
    public string Message { get; set; }
    public int StatusCode { get; set; }
    public AcademicPeriodResponse Result { get; set; }
}