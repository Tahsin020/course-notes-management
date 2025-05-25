using CourseNotesManagement.Application.Common;
using MediatR;

namespace CourseNotesManagement.Application.Features.Notes.Commands.Delete
{
    public class DeleteNoteCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public DeleteNoteCommand(Guid id) => Id = id;
    }
}
