namespace CourseNotesManagement.Application.Features.Teachers.Queries.GetTeacherById
{
    public class CourseAssignmentDto
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public string? CourseName { get; set; }
        public DateTime AssignmentDate { get; set; }
    }
}