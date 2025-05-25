namespace CourseNotesManagement.Application.Features.Students.Queries.GetStudentById
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string TcNo { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public Guid? ParentId { get; set; }
        public string? ParentFullName { get; set; }
    }
}
