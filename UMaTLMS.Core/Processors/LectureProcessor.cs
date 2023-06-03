namespace UMaTLMS.Core.Processors;

[Processor]
public class LectureProcessor
{
	private readonly ILectureRepository _lectureRepository;

	public LectureProcessor(ILectureRepository lectureRepository)
    {
		_lectureRepository = lectureRepository;
	}

	public async Task<bool> Combine(int firstLectureId, int secondLectureId)
	{
		var firstLecture = await _lectureRepository.FindByIdAsync(firstLectureId);
		var secondLecture = await _lectureRepository.FindByIdAsync(secondLectureId);
		if (firstLecture == null || secondLecture == null) return false;
		if (firstLecture.Duration != secondLecture.Duration) return false;
		if (firstLecture.CourseId != secondLecture.CourseId) return false;
		if (firstLecture.CourseNo != secondLecture.CourseNo) return false;
		if (firstLecture.LecturerId != secondLecture.LecturerId) return false;

		var groups = firstLecture.SubClassGroups;
		foreach (var group in groups)
		{
			secondLecture.AddGroup(group);
		}

		await _lectureRepository.DeleteAsync(firstLecture, false);
		return await _lectureRepository.SaveChanges();
	}
}

public record LectureDto(int Id, int LecturerId, int CourseId, int Duration, string CourseNo, 
	bool IsPractical, List<SubClassGroup> SubClassGroups);
