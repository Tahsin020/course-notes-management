using MediatR;

namespace CourseNotesManagement.Application.Features.Auth.Commands
{
    public class LoginCommand : IRequest<LoginResultDto>
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
