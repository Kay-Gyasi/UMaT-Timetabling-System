namespace UMaTLMS.Core.Contracts;

public class Staff
{
    public int Id { get; set; }
    public string StaffNumber { get; set; }
    public string ProfileUrl { get; set; }
    public int DepartmentId { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public Party Party { get; set; }
}

public class Party
{
    public Name? Name { get; set; }
    public EmailAddressValueTypeResponse? PrimaryEmailAddress { get; set; }
    public EmailAddressValueTypeResponse? OtherEmailAddress { get; set; }
}

public class Name
{
    public int Sex { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? OtherName { get; set; }
    public string? FullName { get; set; }
    public string? FullNamev2 { get; set; }
    public string? FullNamev3 { get; set; }
    public int SalutationId { get; set; }
}

public class EmailAddressValueTypeResponse
{
    public Email Email { get; set; }
}

public class Email
{
    public int EmailType { get; set; }
    public string? Value { get; set; }
}