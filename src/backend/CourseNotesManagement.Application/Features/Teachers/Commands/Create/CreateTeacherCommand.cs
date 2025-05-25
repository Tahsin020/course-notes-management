using CourseNotesManagement.Application.Common;
using MediatR;

namespace CourseNotesManagement.Application.Features.Teachers.Commands.Create
{
    public class CreateTeacherCommand : IRequest<Result<Guid>>
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!; // Düz metin şifre
        public string TcNo { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        // Role kullanıcıdan alınmaz, handler'da sabit "Teacher" olarak atanır!
    }
}