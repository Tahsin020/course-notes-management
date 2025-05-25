using CourseNotesManagement.Domain.Common;

namespace CourseNotesManagement.Domain.Entities;

public class CourseAssignment : BaseEntity
{
    public Guid TeacherId { get; set; }
    public Teacher Teacher { get; set; } = default!;
    public Guid CourseId { get; set; }
    public Course Course { get; set; } = default!;
    public DateTime AssignmentDate { get; set; }
}