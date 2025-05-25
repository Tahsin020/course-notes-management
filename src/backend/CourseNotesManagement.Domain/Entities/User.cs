using CourseNotesManagement.Domain.Common;

namespace CourseNotesManagement.Domain.Entities;

public abstract class User : BaseEntity
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public string TcNo { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Role { get; set; } = default!; // "Teacher", "Student", "Parent", "Admin"
}