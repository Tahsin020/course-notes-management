using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Application.Features.Parents.Queries.GetParentById;
using MediatR;

namespace CourseNotesManagement.Application.Features.Parents.Queries.GetAllParents
{
    public class GetAllParentsQuery : IRequest<Result<List<ParentDto>>>
    {
    }
}
