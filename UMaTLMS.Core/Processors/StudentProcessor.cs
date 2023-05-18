using UMaTLMS.Core.Exceptions;

namespace UMaTLMS.Core.Processors;

[Processor]
public class StudentProcessor
{
    private readonly IStudentRepository _studentRepository;
    private readonly IUserRepository _userRepository;

    public StudentProcessor(IStudentRepository studentRepository, IUserRepository userRepository)
    {
        _studentRepository = studentRepository;
        _userRepository = userRepository;
    }

    public async Task<OneOf<int, Exception>> UpsertAsync(StudentCommand command)
    {
        var isNew = command.Id is null;
        Student? student;

        if (isNew)
        {
            if (command.User is null) return new NullReferenceException();
            var userExists = await _userRepository.EmailExists(command.User.Email);
            if (userExists) return new EntityExistsException();

            var user = command.User.Adapt<User>();
            student = Student.Create(command.ClassId)
                .IsUser(User.Create(user));

            var passwordInfo = _userRepository.GetPasswordInfo(command.User.Password ?? "");
            user.SetPassword(passwordInfo.Item1, passwordInfo.Item2);

            await _studentRepository.AddAsync(student);
            return student.Id;
        }

        student = await _studentRepository.FindByIdAsync(command.Id!.Value);
        if (student is null) return new NullReferenceException();

        student.IsInClass(command.ClassId);
        student.User.WithEmail(command.User?.Email)
            .WithPhone(command.User?.PhoneNumber)
            .WasBornOn(command.User?.DateOfBirth)
            .HasUserName(string.Join(" ", command.User?.FirstName, command.User?.LastName))
            .HasFirstName(command.User?.FirstName)
            .HasLastName(command.User?.LastName);
        await _studentRepository.UpdateAsync(student);
        return student.Id;
    }

    public async Task<OneOf<StudentDto, Exception>> GetAsync(int id)
    {
        var lecturer = await _studentRepository.FindByIdAsync(id);
        if (lecturer is null) return new NullReferenceException();

        return lecturer.Adapt<StudentDto>();
    }

    public async Task<PaginatedList<StudentPageDto>> GetPageAsync(PaginatedCommand command)
    {
        var page = await _studentRepository.GetPageAsync(command);
        return page.Adapt<PaginatedList<StudentPageDto>>();
    }

    public async Task DeleteAsync(int id)
    {
        var lecturer = await _studentRepository.FindByIdAsync(id);
        if (lecturer is null) return;

        await _studentRepository.SoftDeleteAsync(lecturer);
    }

    public async Task HardDeleteAsync(int id)
    {
        var lecturer = await _studentRepository.FindByIdAsync(id);
        if (lecturer is null) return;

        await _studentRepository.DeleteAsync(lecturer);
    }
}

public record StudentCommand(int? Id, int UserId, int ClassId, UserCommand? User);
public record StudentDto(int Id, int UserId, int ClassId, UserDto User, ClassDto Class);
public record StudentPageDto(int Id, int UserId, int ClassId, UserPageDto User, ClassPageDto Class);
