namespace CourseNotesManagement.Domain.Entities;

public class Teacher : User
{
    public ICollection<CourseAssignment> CourseAssignments { get; set; } = new List<CourseAssignment>();
}