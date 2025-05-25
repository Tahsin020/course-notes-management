namespace CourseNotesManagement.Application.Features.Parents.Queries.GetParentById
{
    public class ParentDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string TcNo { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public List<ChildStudentDto> Children { get; set; } = [];
    }
}