using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CourseNotesManagement.Application.Features.Notes.Queries.GetNoteById
{
    public class GetNoteByIdQueryHandler : IRequestHandler<GetNoteByIdQuery, Result<NoteDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetNoteByIdQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<NoteDto>> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
        {
            var note = await _context.Notes
                .Include(n => n.Sender)
                .Include(n => n.Receiver)
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == request.Id, cancellationToken);

            if (note == null)
                return Result<NoteDto>.Fail("Not bulunamadı.");

            var dto = new NoteDto
            {
                Id = note.Id,
                SenderId = note.SenderId,
                SenderName = $"{note.Sender.FirstName} {note.Sender.LastName}",
                ReceiverId = note.ReceiverId,
                ReceiverName = $"{note.Receiver.FirstName} {note.Receiver.LastName}",
                Content = note.Content,
                SentAt = note.SentAt
            };

            return Result<NoteDto>.Ok(dto);
        }
    }
}
