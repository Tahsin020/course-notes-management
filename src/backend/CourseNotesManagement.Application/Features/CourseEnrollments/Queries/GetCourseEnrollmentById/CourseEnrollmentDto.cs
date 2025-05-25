namespace CourseNotesManagement.Application.Features.CourseEnrollments.Queries.GetCourseEnrollmentById
{
    public class CourseEnrollmentDto
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string StudentName { get; set; } = default!;
        public Guid CourseId { get; set; }
        public string CourseName { get; set; } = default!;
        public DateTime EnrollmentDate { get; set; }
    }
}
