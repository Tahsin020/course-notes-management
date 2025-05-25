namespace CourseNotesManagement.Application.Features.Parents.Queries.GetParentById
{
    public class ChildStudentDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
    }

}
