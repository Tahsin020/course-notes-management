using CourseNotesManagement.Application.Common;
using MediatR;

namespace CourseNotesManagement.Application.Features.Notes.Commands.Update
{
    public class UpdateNoteCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = default!;
        // Notun Sender/Receiver'ı güncellenmeyecek, sadece içerik!
    }
}
