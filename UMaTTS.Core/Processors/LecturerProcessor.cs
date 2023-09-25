using Humanizer;
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

    public async Task<OneOf<int, Exception>> UpsertAsync(LecturerCommand command)
    {
        bool isNew = command.Id == 0;
        Lecturer? lecturer;
        
        if (isNew)
        {
            lecturer = Lecturer.Create(command.UmatId, command.Name, command.TitledName);
            await _lecturerRepository.AddAsync(lecturer);
            return lecturer.Id;
        }

        lecturer = await _lecturerRepository.FindByIdAsync(command.Id);
        if (lecturer is null) return new InvalidIdException();
        lecturer.HasName(command.Name, command.TitledName);
        await _lecturerRepository.UpdateAsync(lecturer);
        return lecturer.Id;
    }

    public async Task<PaginatedList<LecturerDto>> GetPageAsync(PaginatedCommand command)
    {
        var page = await _lecturerRepository.GetPageAsync(command);
        return page.Adapt<PaginatedList<LecturerDto>>(Mapping.GetTypeAdapterConfig());
    }

    public async Task<PaginatedList<PreferenceDto>> GetPreferences(PaginatedCommand command)
    {
        var preferences = await _preferenceRepository.GetLecturerPreferences(command);
        List<PreferenceDto> dtoData = new();
        foreach (var data in preferences.Data)
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
                case PreferenceType.PreferredLectureRoom:
                    var p4 = JsonSerializer.Deserialize<PreferredLectureRoom>(data.Value);
                    if (p4 is null) break;
                    value = p4.Room.Humanize();
                    break;
                default:
                    break;
            }

            dtoData.Add(new PreferenceDto(data.Id, data.Type.Humanize(), value,
                            data.TimetableType.Humanize(), data.Lecturer!.Name, null));
        }

        return new PaginatedList<PreferenceDto>(dtoData, preferences.TotalCount, preferences.CurrentPage, preferences.PageSize);
    }

    public async Task DeleteAsync(int id)
    {
        var room = await _lecturerRepository.FindByIdAsync(id);
        if (room is null) return;

        await _lecturerRepository.SoftDeleteAsync(room);
    }
}

public record LecturerDto(int Id, int UmatId, string Name);
public record LecturerCommand(int Id, int UmatId, string Name, string? TitledName);
