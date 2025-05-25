using CourseNotesManagement.Application.Common;
using MediatR;
using static CourseNotesManagement.Application.Features.Admins.Queries.GetAdminById.GetAllAdminsQueryHandler;

namespace CourseNotesManagement.Application.Features.Admins.Queries.GetAllAdmins
{
    public class GetAllAdminsQuery : IRequest<Result<List<AdminDto>>>
    {
    }
}
