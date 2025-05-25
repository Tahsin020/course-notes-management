namespace CourseNotesManagement.Application.Features.Teachers.Queries.GetTeacherById
{
    public class TeacherDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string TcNo { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Role { get; set; } = default!;
        public List<CourseAssignmentDto> CourseAssignments { get; set; } = [];
    }
}