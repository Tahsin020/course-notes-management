namespace CourseNotesManagement.Application.Features.Auth.Commands
{
    public class LoginResultDto
    {
        public string Token { get; set; } = default!;
        public DateTime Expiration { get; set; }
        public Guid UserId { get; set; }
        public string FullName { get; set; } = default!;
        public string Role { get; set; } = default!;
    }
}
