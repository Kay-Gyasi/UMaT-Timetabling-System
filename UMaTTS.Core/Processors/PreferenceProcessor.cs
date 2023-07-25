namespace UMaTLMS.Core.Processors;
public class PreferenceProcessor
{
    private readonly IPreferenceRepository _preferenceRepository;

    public PreferenceProcessor(IPreferenceRepository preferenceRepository)
    {
        _preferenceRepository = preferenceRepository;
    }


}

public class DayNotAvailable
{
    public DayOfWeek Day { get; set; }
}

public class TimeNotAvailable
{
    public DayOfWeek? Day { get; set; }
    public List<string>? TimesNotAvailable { get; set; }
}