using CourseNotesManagement.Application.Common;
using MediatR;

namespace CourseNotesManagement.Application.Features.Parents.Queries.GetParentById
{
    public class GetParentByIdQuery : IRequest<Result<ParentDto>>
    {
        public Guid Id { get; set; }
        public GetParentByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
