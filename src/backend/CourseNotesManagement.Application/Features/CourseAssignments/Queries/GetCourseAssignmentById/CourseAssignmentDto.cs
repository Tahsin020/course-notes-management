namespace CourseNotesManagement.Application.Features.CourseAssignments.Queries.GetCourseAssignmentById
{
    public class CourseAssignmentDto
    {
        public Guid Id { get; set; }
        public Guid TeacherId { get; set; }
        public string TeacherName { get; set; } = default!;
        public Guid CourseId { get; set; }
        public string CourseName { get; set; } = default!;
        public DateTime AssignmentDate { get; set; }
    }
}
