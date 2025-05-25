using CourseNotesManagement.Application.Common;
using MediatR;

namespace CourseNotesManagement.Application.Features.Teachers.Commands.Update
{
    public class UpdateTeacherCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? Password { get; set; } // null ise şifre değişmeyecek
        public string TcNo { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
    }
}
