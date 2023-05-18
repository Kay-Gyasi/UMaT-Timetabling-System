using UMaTLMS.Infrastructure.Persistence.Repositories.Base;

namespace UMaTLMS.Core.Repositories;

public interface IUserRepository : IRepository<User, int> 
{
    Task<bool> EmailExists(string? email);
    Task<User?> FindByEmailAsync(string? email);
    (byte[], byte[]) GetPasswordInfo(string password);
    bool MatchPasswordHash(string passwordText, IReadOnlyList<byte> password, byte[] passwordKey);
}
