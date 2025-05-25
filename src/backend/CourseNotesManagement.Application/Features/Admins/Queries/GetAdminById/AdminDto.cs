namespace CourseNotesManagement.Application.Features.Admins.Queries.GetAdminById;

public partial class GetAllAdminsQueryHandler
{
    public class AdminDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string TcNo { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
    }
}
