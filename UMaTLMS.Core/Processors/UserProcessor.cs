namespace UMaTLMS.Core.Processors;

[Processor]
public class UserProcessor
{
    private readonly ITokenService _tokenService;
    private readonly ILogger<UserProcessor> _logger;
    private readonly IUserRepository _userRepository;

    public UserProcessor(ITokenService tokenService, ILogger<UserProcessor> logger,
        IUserRepository userRepository)
    {
        _tokenService = tokenService;
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task<OneOf<AuthToken, Exception>> LoginAsync(LoginCommand command)
    {
        var user = await _userRepository.FindByEmailAsync(command.Email);
        if (user == null) return new InvalidLoginException();

        var isValidPassword = await Task.Run(() => _userRepository.MatchPasswordHash(command.Password, user.Password!, user.PasswordKey!));

        if (!isValidPassword) return new InvalidLoginException();

        return await Task.Run(() => _tokenService.GenerateToken(user));
    }
}

public record SignupCommand(UserType Type, string FirstName, string LastName,
    string? Email, string? PhoneNumber, string Password);

public record LoginCommand(string Email, string Password);

public record UserCommand(int? Id, UserType Type, string FirstName, string LastName, DateTime? DateOfBirth,
    string? UserName, string? Email, string? PhoneNumber, string? Password);

public record UserDto(int Id, UserType Type, string FirstName, string LastName, DateTime? DateOfBirth,
    string? UserName, string? Email, string? PhoneNumber);

public record UserPageDto(int Id, UserType Type, string FirstName, string LastName, DateTime? DateOfBirth,
    string? UserName, string? Email, string? PhoneNumber);
