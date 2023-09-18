using Humanizer;
using System.Text.Json;
using UMaTLMS.Core.Enums;

namespace UMaTLMS.Core.Processors;

[Processor]
public class PreferenceProcessor
{
    private readonly IPreferenceRepository _preferenceRepository;
    private readonly IAdminSettingsRepository _adminSettingsRepository;
    private readonly LookupProcessor _lookupProcessor;

    public PreferenceProcessor(IPreferenceRepository preferenceRepository, 
        IAdminSettingsRepository adminSettingsRepository, LookupProcessor lookupProcessor)
    {
        _preferenceRepository = preferenceRepository;
        _adminSettingsRepository = adminSettingsRepository;
        _lookupProcessor = lookupProcessor;
    }


    public async Task<OneOf<bool, Exception>> Set(PreferenceCommand command)
    {
        try
        {
            if (command.Lecturers is null && command.Courses is null) return new NullReferenceException();

            var preferenceTypes = Enum.GetValues<PreferenceType>();
            var timetableTypes = Enum.GetValues<TimetableType>();

            var preferenceType = preferenceTypes[command.Type - 1];

            if (command.Lecturers is not null)
            {
                foreach (var lecturerId in command.Lecturers)
                {
                    foreach (var value in command.Values)
                    {
                        Preference? preference;
                        string result = string.Empty;
                        switch (preferenceType)
                        {
                            case PreferenceType.DayNotAvailable:
                                var v1 = new DayNotAvailable() { Day = Enum.Parse<DayOfWeek>(value) };
                                result = JsonSerializer.Serialize(v1);
                                break;
                            case PreferenceType.TimeNotAvailable:
                                var v3 = new TimeNotAvailable() { Day = Enum.Parse<DayOfWeek>(command.DayForTimeNotAvailable ?? ""), Time = value };
                                result = JsonSerializer.Serialize(v3);
                                break;
                            case PreferenceType.PreferredDayOfWeek:
                                var v2 = new PreferredDayOfWeek() { Day = Enum.Parse<DayOfWeek>(value) };
                                result = JsonSerializer.Serialize(v2);
                                break;
                            case PreferenceType.PreferredLectureRoom:
                                var v4 = new PreferredLectureRoom() { Room = value };
                                result = JsonSerializer.Serialize(v4);
                                break;
                            default:
                                break;
                        }

                        preference = Preference.Create(preferenceTypes[command.Type - 1], timetableTypes[command.TimetableType - 1], lecturerId)
                                                    .WithValue(result);
                        await _preferenceRepository.AddAsync(preference, saveChanges: false);
                    }
                }
            }
            
            if (command.Courses is not null)
            {
                foreach (var courseId in command.Courses)
                {
                    foreach (var value in command.Values)
                    {
                        Preference? preference;
                        string result = string.Empty;
                        switch (preferenceType)
                        {
                            case PreferenceType.DayNotAvailable:
                                var v1 = new DayNotAvailable() { Day = Enum.Parse<DayOfWeek>(value) };
                                result = JsonSerializer.Serialize(v1);
                                break;
                            case PreferenceType.TimeNotAvailable:
                                var v3 = new TimeNotAvailable() { Day = Enum.Parse<DayOfWeek>(command.DayForTimeNotAvailable ?? ""), Time = value };
                                break;
                            case PreferenceType.PreferredDayOfWeek:
                                var v2 = new PreferredDayOfWeek() { Day = Enum.Parse<DayOfWeek>(value) };
                                result = JsonSerializer.Serialize(v2);
                                break;
                            case PreferenceType.PreferredLectureRoom:
                                var v4 = new PreferredLectureRoom() { Room = value };
                                result = JsonSerializer.Serialize(v4);
                                break;
                            default:
                                break;
                        }

                        preference = Preference.Create(preferenceTypes[command.Type - 1], timetableTypes[command.TimetableType - 1], courseId: courseId)
                                                    .WithValue(result);
                        await _preferenceRepository.AddAsync(preference, saveChanges: false);
                    }
                }
            }

            return await _preferenceRepository.SaveChanges();
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public async Task DeleteAsync(int id)
    {
        var room = await _preferenceRepository.FindByIdAsync(id);
        if (room is null) return;

        await _preferenceRepository.DeleteAsync(room);
    }

    public async Task<List<Lookup>> GetTypeValues(int type)
    {
        var selectedValue = Enum.GetValues<PreferenceType>()[type];
        var index = 0;

        var timeSlotSetting = await _adminSettingsRepository.GetAsync(x => x.Key == AdminConfigurationKeys.LectureTimeSlots);
        if (timeSlotSetting is null) throw new SystemNotInitializedException("Timeslots not set");
        var timeSlots = JsonSerializer.Deserialize<List<string>>(timeSlotSetting.Value) ?? new List<string>();

        var dayOfWeekSetting = await _adminSettingsRepository.GetAsync(x => x.Key == AdminConfigurationKeys.DaysOfWeek);
        if (dayOfWeekSetting is null) throw new SystemNotInitializedException("Days of week not set");
        var daysOfWeek = JsonSerializer.Deserialize<List<string>>(dayOfWeekSetting.Value) ?? new List<string>();

        return selectedValue switch
        {
            PreferenceType.DayNotAvailable => daysOfWeek.Select(x => new Lookup(index++, x)).ToList(),
            PreferenceType.TimeNotAvailable => timeSlots.Select(x => new Lookup(index++, x)).ToList(),
            PreferenceType.PreferredDayOfWeek => daysOfWeek.Select(x => new Lookup(index++, x)).ToList(),
            PreferenceType.PreferredLectureRoom => await _lookupProcessor.GetAsync(LookupType.Rooms),
            _ => throw new NotImplementedException()
        };
    }

    public static PreferenceLookups GetTypes()
    {
        var index = 0;
        var preferenceTypes = Enum.GetValues<PreferenceType>().Select(x => new Lookup(++index, x.Humanize())).ToList();
        index = 0;
        var timetableTypes = Enum.GetValues<TimetableType>().Select(x => new Lookup(++index, x.Humanize())).ToList();

        return new PreferenceLookups(preferenceTypes, timetableTypes);
    }
}

public record PreferenceCommand(int Type, List<string> Values, int TimetableType, string? DayForTimeNotAvailable, List<int>? Lecturers, List<int>? Courses);
public record PreferenceDto(int Id, string Type, string Value, string TimetableType, string? Lecturer,
    string? Course);

public record PreferenceLookups(List<Lookup> PreferenceTypes, List<Lookup> TimetableTypes);

public interface IPreference { }

public class DayNotAvailable : IPreference
{
    public DayOfWeek Day { get; set; }
}

public class PreferredDayOfWeek : IPreference
{
    public DayOfWeek Day { get; set; }
}

public class TimeNotAvailable : IPreference
{
    public DayOfWeek? Day { get; set; }
    public string Time { get; set; }
}

public class PreferredLectureRoom : IPreference
{
    public string Room { get; set; }
}