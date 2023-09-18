namespace UMaTLMS.Core.Entities;
public class AdminSettings : Entity
{
    private AdminSettings(AdminConfigurationKeys key, string value)
    {
        Key = key;
        Value = value;
    }

    public AdminConfigurationKeys Key { get; private set; }
    public string Value { get; private set; }

    public static AdminSettings Create(AdminConfigurationKeys key, string value) 
        => new(key, value);

    public AdminSettings HasValue(string value)
    {
        Value = value;
        return this;
    }
}

public enum AdminConfigurationKeys
{
    GeneralClassSizeLimit,
    LectureTimeSlots,
    DaysOfWeek
}
