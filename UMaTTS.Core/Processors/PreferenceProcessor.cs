using Humanizer;
using System.Text.Json;
using UMaTLMS.Core.Enums;
using UMaTLMS.Core.Helpers;

namespace UMaTLMS.Core.Processors;

[Processor]
public class PreferenceProcessor
{
    private readonly IPreferenceRepository _preferenceRepository;

    public PreferenceProcessor(IPreferenceRepository preferenceRepository)
    {
        _preferenceRepository = preferenceRepository;
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

    public async Task<PaginatedList<PreferenceDto>> GetPageAsync(PaginatedCommand command)
    {
        var page = await _preferenceRepository.GetPageAsync(command);
        List<PreferenceDto> dtoData = new();
        foreach (var data in page.Data)
        {
            var value = string.Empty;
            switch (data.Type)
            {
                case PreferenceType.DayNotAvailable:
                    var p1 = JsonSerializer.Deserialize<DayNotAvailable>(data.Value);
                    if (p1 is null) break;
                    value = p1.Day.Humanize();
                    break;
                case PreferenceType.TimeNotAvailable:
                    var p2 = JsonSerializer.Deserialize<TimeNotAvailable>(data.Value);
                    if (p2 is null) break;
                    value = string.Join(":", p2.Day.Humanize(), p2.Time);
                    break;
                case PreferenceType.PreferredDayOfWeek:
                    var p3 = JsonSerializer.Deserialize<PreferredDayOfWeek>(data.Value);
                    if (p3 is null) break;
                    value = p3.Day.Humanize();
                    break;
                default:
                    break;
            }

            dtoData.Add(new PreferenceDto(data.Id, data.Type.Humanize(), value,
                            data.TimetableType.Humanize(), data.Lecturer!.Name, data.Course!.Name));
        }
        return new PaginatedList<PreferenceDto>(dtoData, page.TotalCount, page.CurrentPage, page.PageSize);
    }

    public async Task DeleteAsync(int id)
    {
        var room = await _preferenceRepository.FindByIdAsync(id);
        if (room is null) return;

        await _preferenceRepository.DeleteAsync(room);
    }

    public static List<Lookup> GetTypeValues(int type)
    {
        var selectedValue = Enum.GetValues<PreferenceType>()[type];
        var index = 0;
        return selectedValue switch
        {
            PreferenceType.DayNotAvailable => AppHelpers.GetDaysOfWeek().Select(x => new Lookup(index++, x)).ToList(),
            PreferenceType.TimeNotAvailable => AppHelpers.GetTimeSlots().Select(x => new Lookup(index++, x)).ToList(),
            PreferenceType.PreferredDayOfWeek => AppHelpers.GetDaysOfWeek().Select(x => new Lookup(index++, x)).ToList(),
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