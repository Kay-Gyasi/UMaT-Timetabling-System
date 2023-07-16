using UMaTLMS.Core.Helpers;

namespace UMaTLMS.Core.Processors;

[Processor]
public class LectureProcessor
{
	private readonly ILectureRepository _lectureRepository;
	private readonly ISubClassGroupRepository _subClassGroupRepository;

	public LectureProcessor(ILectureRepository lectureRepository, ISubClassGroupRepository subClassGroupRepository)
	{
		_lectureRepository = lectureRepository;
		_subClassGroupRepository = subClassGroupRepository;
	}

	public async Task<OneOf<bool, Exception>> CreateCombined(List<LectureCommand> lectures)
	{
		var lectureId = lectures.FirstOrDefault()?.Id ?? 0;
		if (lectureId == 0) return new InvalidIdException();

		var lecture = await _lectureRepository.FindByIdAsync(lectureId);
		if (lecture is null) return new InvalidIdException();
		lecture.Delete();

		try
		{
			foreach (var x in lectures)
			{
				var lect = Lecture.Create(x.LecturerId, x.CourseId, lecture.Duration, x.IsPractical);
				if (x.IsVLE) lect.IsHeldOnline();
				if (x.PreferredRoom is not null) lect.HasPreferredRoom(x.PreferredRoom);

				foreach (var id in x.SubClassGroups.Select(g => g.Id))
				{
					if (id is null or 0) continue;
					var group = await _subClassGroupRepository.FindByIdAsync(id ?? 0);
					lect.AddGroup(group);
				}

				await _lectureRepository.AddAsync(lect, false);
			}

			await _lectureRepository.SaveChanges();
		}
		catch (Exception ex)
		{
			return ex;
		}

		return true;
	}

	public async Task<PaginatedList<LecturePageDto>> GetPageAsync(PaginatedCommand command) 
	{
		var page = await _lectureRepository.GetPageAsync(command);
		return page.Adapt<PaginatedList<LecturePageDto>>(Mapping.GetTypeAdapterConfig());
	}

	public async Task<OneOf<LectureDto, Exception>> GetAsync(int id)
	{
		var lecture = await _lectureRepository.FindByIdAsync(id);
		if (lecture is null) return new InvalidIdException();
		return lecture.Adapt<LectureDto>(Mapping.GetTypeAdapterConfig());
	}
}

public record LectureCommand(int Id, int LecturerId, int CourseId, string? PreferredRoom, int Duration, bool IsPractical, bool  IsVLE, 
	 List<SubClassGroupCommand> SubClassGroups);

public record LectureDto(int Id, int LecturerId, int CourseId, string PreferredRoom, int Duration, bool IsPractical, bool  IsVLE, 
	 LecturerDto Lecturer, CourseDto Course, List<SubClassGroupPageDto> SubClassGroups);

public record LecturerDto(int Id, int UmatId, string Name);
public record LecturePageDto(int Id, string Lecturer, string Course, bool IsPractical, bool IsVLE, List<string> Classes);
