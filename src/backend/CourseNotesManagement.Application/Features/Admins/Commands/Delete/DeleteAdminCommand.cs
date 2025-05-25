using CourseNotesManagement.Application.Common;
using MediatR;

namespace CourseNotesManagement.Application.Features.Admins.Commands.Delete;

public class DeleteAdminCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public DeleteAdminCommand(Guid id)
    {
        Id = id;
    }
}
