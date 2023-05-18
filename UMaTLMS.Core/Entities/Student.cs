namespace UMaTLMS.Core.Entities;

public class Student : Entity
{
    private Student(int classId)
    {
        ClassId = classId;
    }

    public int UserId { get; private set; }
    public int? ClassId { get; private set; }
    public Class? Class { get; private set; }
    public User User { get; private set; }

    public static Student Create(int classId) => new(classId);

    public Student IsInClass(int classId)
    {
        ClassId = classId;
        return this;
    }

    public Student IsUser(User user)
    {
        User = user;
        UserId = user.Id;
        return this;
    }
}