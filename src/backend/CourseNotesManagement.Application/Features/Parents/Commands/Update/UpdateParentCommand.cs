using CourseNotesManagement.Application.Common;
using MediatR;

namespace CourseNotesManagement.Application.Features.Parents.Commands.Update
{
    public class UpdateParentCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? Password { get; set; }
        public string TcNo { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
    }
}
