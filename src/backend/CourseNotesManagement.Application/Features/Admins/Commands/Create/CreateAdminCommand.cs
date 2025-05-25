using CourseNotesManagement.Application.Common;
using MediatR;

namespace CourseNotesManagement.Application.Features.Admins.Commands.Create;

public class CreateAdminCommand : IRequest<Result<Guid>>
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string TcNo { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    // Role handler'da sabitlenir ("Admin")
}