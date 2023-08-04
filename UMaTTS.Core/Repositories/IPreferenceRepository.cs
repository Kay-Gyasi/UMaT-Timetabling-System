using UMaTLMS.Core.Repositories.Base;

namespace UMaTLMS.Core.Repositories;

public interface IPreferenceRepository : IRepository<Preference, int>
{
    Task<PaginatedList<Preference>> GetLecturerPreferences(PaginatedCommand command);
    Task<PaginatedList<Preference>> GetCoursePreferences(PaginatedCommand command);
}
