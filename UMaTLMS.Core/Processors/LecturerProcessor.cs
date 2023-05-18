using UMaTLMS.Core.Exceptions;

namespace UMaTLMS.Core.Processors;

[Processor]
public class LecturerProcessor
{
    private readonly ILecturerRepository _lecturerRepository;
    private readonly IUserRepository _userRepository;

    public LecturerProcessor(ILecturerRepository lecturerRepository, IUserRepository userRepository)
    {
        _lecturerRepository = lecturerRepository;
        _userRepository = userRepository;
    }

    public async Task<OneOf<int, Exception>> UpsertAsync(LecturerCommand command)
    {
        var isNew = command.Id is null;
        Lecturer? lecturer;

        if (isNew)
        {
            if (command.User is null) return new NullReferenceException();
            var userExists = await _userRepository.EmailExists(command.User.Email);
            if (userExists) return new EntityExistsException();

            var user = command.User.Adapt<User>();

            lecturer = Lecturer.Create(user)
                .BelongsTo(command.DepartmentId);

            var passwordInfo = _userRepository.GetPasswordInfo(command.User.Password ?? "");
            user.SetPassword(passwordInfo.Item1, passwordInfo.Item2);

            await _lecturerRepository.AddAsync(lecturer);
            return lecturer.Id;
        }

        lecturer = await _lecturerRepository.FindByIdAsync(command.Id!.Value);
        if (lecturer is null) return new NullReferenceException();

        lecturer.BelongsTo(command.DepartmentId);
        lecturer.User.WithEmail(command.User?.Email)
            .WithPhone(command.User?.PhoneNumber)
            .WasBornOn(command.User?.DateOfBirth)
            .HasUserName(string.Join(" ", command.User?.FirstName, command.User?.LastName))
            .HasFirstName(command.User?.FirstName)
            .HasLastName(command.User?.LastName);

        await _lecturerRepository.UpdateAsync(lecturer);
        return lecturer.Id;
    }

    public async Task<OneOf<LecturerDto, Exception>> GetAsync(int id)
    {
        var lecturer = await _lecturerRepository.FindByIdAsync(id);
        if (lecturer is null) return new NullReferenceException();

        return lecturer.Adapt<LecturerDto>();
    }

    public async Task<PaginatedList<LecturerPageDto>> GetPageAsync(PaginatedCommand command)
    {
        var page = await _lecturerRepository.GetPageAsync(command);
        return page.Adapt<PaginatedList<LecturerPageDto>>();
    }

    public async Task DeleteAsync(int id)
    {
        var lecturer = await _lecturerRepository.FindByIdAsync(id);
        if (lecturer is null) return;

        await _lecturerRepository.SoftDeleteAsync(lecturer);
    }

    public async Task HardDeleteAsync(int id)
    {
        var lecturer = await _lecturerRepository.FindByIdAsync(id);
        if (lecturer is null) return;

        await _lecturerRepository.DeleteAsync(lecturer);
    }
}

public record LecturerCommand(int? Id, int? DepartmentId, int UserId, UserCommand? User);

public record LecturerDto(int Id, int DepartmentId, int UserId, UserDto User);

public record LecturerPageDto(int Id, int DepartmentId, int UserId, UserPageDto User);
