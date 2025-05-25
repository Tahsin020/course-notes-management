namespace CourseNotesManagement.Domain.Entities;

public class Student : User
{
    public Guid? ParentId { get; set; }
    public Parent? Parent { get; set; }
    public ICollection<CourseEnrollment> CourseEnrollments { get; set; } = new List<CourseEnrollment>();
}