using CourseNotesManagement.Application.Common;
using MediatR;

namespace CourseNotesManagement.Application.Features.Students.Commands.Update
{
    public class UpdateStudentCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? Password { get; set; } // null veya boş ise şifre değişmez
        public string TcNo { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public Guid? ParentId { get; set; }
    }
}
