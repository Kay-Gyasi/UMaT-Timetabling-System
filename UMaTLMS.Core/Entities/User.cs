namespace UMaTLMS.Core.Entities;

public sealed class User : Entity
{
    private User(UserType type, string firstName, string lastName)
    {
        Type = type;
        FirstName = firstName;
        LastName = lastName;
        UserName = string.Join("", FirstName[..3], LastName);
    }
    
    private User(User user)
    {
        Type = user.Type;
        FirstName = user.FirstName;
        LastName =user.LastName;
        UserName = string.Join("", FirstName[..3], LastName);
        PhoneNumber = user.PhoneNumber;
        Email = user.Email;
        DateOfBirth = user.DateOfBirth;
    }

    public UserType Type { get; private set; }
    public string FirstName { get; private set; }
    public string? UserName { get; private set; }
    public string? PhoneNumber { get; private set; }
    public string? Email { get; private set; }
    public string LastName { get; private set; }
    public bool IsAdmin { get; private set; } = false;
    public DateTime? DateOfBirth { get; private set; }
    public byte[]? Password { get; private set; }
    public byte[]? PasswordKey { get; private set; }



    public static User Create(UserType type, string firstName, string lastName)
    {
        return new(type, firstName, lastName);
    }
    
    public static User Create(User user)
    {
        return new(user);
    }

    public User OfType(UserType type)
    {
        Type = type;
        return this;
    }

    public User WasBornOn(DateTime? date)
    {
        DateOfBirth = date?.Date;
        return this;
    }

    public User HasFirstName(string? firstName)
    {
        if (string.IsNullOrEmpty(firstName)) return this;
        FirstName = firstName;
        return this;
    }

    public User HasLastName(string? lastName)
    {
        if (string.IsNullOrEmpty(lastName)) return this;
        LastName = lastName;
        return this;
    }

    public User HasUserName(string username)
    {
        if (string.IsNullOrEmpty(username)) return this;
        UserName = username;
        return this;
    }

    public User WithEmail(string? email)
    {
        Email = email;
        return this;
    }

    public User WithPhone(string? phone)
    {
        PhoneNumber = phone;
        return this;
    }

    public User SetPassword(byte[] password, byte[] passwordKey)
    {
        Password = password;
        PasswordKey = passwordKey;
        return this;
    }
}

public enum UserType
{
    Lecturer = 1,
    Student,
    Developer,
    Admin
}
