using UMaTLMS.Core.Repositories.Base;

namespace UMaTLMS.Core.Repositories
{
    public interface ILecturerRepository : IRepository<Lecturer, int>
    {
        Task<bool> Exists(int umatId);
        Task<List<Lecturer>> GetAll();
    }
}