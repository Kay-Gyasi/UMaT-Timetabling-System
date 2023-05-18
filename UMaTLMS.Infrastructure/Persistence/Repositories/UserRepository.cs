using System.Security.Cryptography;
using System.Text;

namespace UMaTLMS.Infrastructure.Persistence.Repositories;

public class UserRepository : Repository<User, int>, IUserRepository
{

    public UserRepository(AppDbContext context, ILogger<Repository<User, int>> logger) : base(context, logger)
    {
    }

    public async Task<bool> EmailExists(string? email)
    {
        if (email == null) return false;
        return await GetBaseQuery().AnyAsync(x => x.Email == email);
    }

    public async Task<User?> FindByEmailAsync(string? email)
    {
        if (email == null) return null;
        return await GetBaseQuery().FirstOrDefaultAsync(x => x.Email == email);
    }

    public (byte[], byte[]) GetPasswordInfo(string password)
    {
        byte[] passwordHash, passwordKey;

        using var hmac = new HMACSHA512();
        passwordKey = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        return (passwordHash, passwordKey);
    }

    public bool MatchPasswordHash(string passwordText, IReadOnlyList<byte> password, byte[] passwordKey)
    {
        using var hmac = new HMACSHA512(passwordKey);
        var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(passwordText));
        return !passwordHash.Where((t, i) => t != password[i]).Any();
    }
}
