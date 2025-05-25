using CourseNotesManagement.Domain.Common;

namespace CourseNotesManagement.Domain.Entities;

public class CourseEnrollment : BaseEntity
{
    public Guid StudentId { get; set; }
    public Student Student { get; set; } = default!;
    public Guid CourseId { get; set; }
    public Course Course { get; set; } = default!;
    public DateTime EnrollmentDate { get; set; }
}