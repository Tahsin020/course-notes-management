using CourseNotesManagement.Application.Common;
using MediatR;

namespace CourseNotesManagement.Application.Features.Parents.Commands.Delete
{
    public class DeleteParentCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public DeleteParentCommand(Guid id)
        {
            Id = id;
        }
    }
}
