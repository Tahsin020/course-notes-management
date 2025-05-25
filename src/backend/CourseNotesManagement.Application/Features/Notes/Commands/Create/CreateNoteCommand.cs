using CourseNotesManagement.Application.Common;
using MediatR;

namespace CourseNotesManagement.Application.Features.Notes.Commands.Create
{
    public class CreateNoteCommand : IRequest<Result<Guid>>
    {
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public string Content { get; set; } = default!;
        public DateTime? SentAt { get; set; }
    }
}
