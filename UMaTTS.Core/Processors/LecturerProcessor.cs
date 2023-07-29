﻿using Humanizer;
using System.Text.Json;
using UMaTLMS.Core.Helpers;

namespace UMaTLMS.Core.Processors;

[Processor]
public class LecturerProcessor
{
    private readonly ILecturerRepository _lecturerRepository;
    private readonly IPreferenceRepository _preferenceRepository;

    public LecturerProcessor(ILecturerRepository lecturerRepository, IPreferenceRepository preferenceRepository)
    {
        _lecturerRepository = lecturerRepository;
        _preferenceRepository = preferenceRepository;
    }

    public async Task<PaginatedList<LecturerDto>> GetPageAsync(PaginatedCommand command)
    {
        var page = await _lecturerRepository.GetPageAsync(command);
        return page.Adapt<PaginatedList<LecturerDto>>(Mapping.GetTypeAdapterConfig());
    }

    public async Task<List<PreferenceDto>> GetPreferences()
    {
        var preferences = await _preferenceRepository.GetLecturerPreferences();
        List<PreferenceDto> dtoData = new();
        foreach (var data in preferences)
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
                            data.TimetableType.Humanize(), data.Lecturer!.Name, null));
        }
        return dtoData;
    }
}

public record LecturerDto(int Id, int UmatId, string Name);
