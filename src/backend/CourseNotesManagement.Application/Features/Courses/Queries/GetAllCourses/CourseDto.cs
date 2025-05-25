namespace CourseNotesManagement.Application.Features.Courses.Queries.GetAllCourses
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public int AssignmentCount { get; set; }
        public int EnrollmentCount { get; set; }
    }
}
