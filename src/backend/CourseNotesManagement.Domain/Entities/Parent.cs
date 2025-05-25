namespace CourseNotesManagement.Domain.Entities;

public class Parent : User
{
    public ICollection<Student> Children { get; set; } = new List<Student>();
}