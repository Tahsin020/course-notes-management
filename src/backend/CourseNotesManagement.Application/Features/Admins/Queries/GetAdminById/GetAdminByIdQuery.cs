using CourseNotesManagement.Application.Common;
using MediatR;
using static CourseNotesManagement.Application.Features.Admins.Queries.GetAdminById.GetAllAdminsQueryHandler;

namespace CourseNotesManagement.Application.Features.Admins.Queries.GetAdminById;

public class GetAdminByIdQuery : IRequest<Result<AdminDto>>
{
    public Guid Id { get; set; }
    public GetAdminByIdQuery(Guid id)
    {
        Id = id;
    }
}
