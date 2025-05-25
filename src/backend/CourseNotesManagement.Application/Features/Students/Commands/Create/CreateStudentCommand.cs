using CourseNotesManagement.Application.Common;
using MediatR;

namespace CourseNotesManagement.Application.Features.Students.Commands.Create;

public class CreateStudentCommand : IRequest<Result<Guid>>
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string TcNo { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public Guid? ParentId { get; set; } // Öğrenciye veli atanabilir
                                        // Role kullanıcıdan alınmaz, handler'da sabit "Student" olarak atanır!
}
