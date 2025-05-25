using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Application.Features.Notes.Queries.GetNoteById;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CourseNotesManagement.Application.Features.Notes.Queries.GetAllNotes
{
    public class GetAllNotesQueryHandler : IRequestHandler<GetAllNotesQuery, Result<List<NoteDto>>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllNotesQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<NoteDto>>> Handle(GetAllNotesQuery request, CancellationToken cancellationToken)
        {
            var notes = await _context.Notes
                .Include(n => n.Sender)
                .Include(n => n.Receiver)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var result = notes.Select(note => new NoteDto
            {
                Id = note.Id,
                SenderId = note.SenderId,
                SenderName = $"{note.Sender.FirstName} {note.Sender.LastName}",
                ReceiverId = note.ReceiverId,
                ReceiverName = $"{note.Receiver.FirstName} {note.Receiver.LastName}",
                Content = note.Content,
                SentAt = note.SentAt
            }).ToList();

            return Result<List<NoteDto>>.Ok(result);
        }
    }
}
