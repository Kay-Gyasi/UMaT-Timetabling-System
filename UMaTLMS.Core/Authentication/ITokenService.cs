namespace UMaTLMS.Core.Authentication;

public interface ITokenService
{
    AuthToken GenerateToken(User user);
}

public record AuthToken(string Token);