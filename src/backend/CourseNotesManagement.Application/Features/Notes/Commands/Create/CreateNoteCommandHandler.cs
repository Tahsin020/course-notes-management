using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Domain.Entities;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CourseNotesManagement.Application.Features.Notes.Commands.Create
{
    public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Result<Guid>>
    {
        private readonly ApplicationDbContext _context;

        public CreateNoteCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Guid>> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            // User tablosu yok, alt entity'lerde kontrol et
            var senderExists =
                await _context.Students.AnyAsync(s => s.Id == request.SenderId, cancellationToken) ||
                await _context.Teachers.AnyAsync(t => t.Id == request.SenderId, cancellationToken) ||
                await _context.Admins.AnyAsync(a => a.Id == request.SenderId, cancellationToken) ||
                await _context.Parents.AnyAsync(p => p.Id == request.SenderId, cancellationToken);

            var receiverExists =
                await _context.Students.AnyAsync(s => s.Id == request.ReceiverId, cancellationToken) ||
                await _context.Teachers.AnyAsync(t => t.Id == request.ReceiverId, cancellationToken) ||
                await _context.Admins.AnyAsync(a => a.Id == request.ReceiverId, cancellationToken) ||
                await _context.Parents.AnyAsync(p => p.Id == request.ReceiverId, cancellationToken);

            if (!senderExists || !receiverExists)
                return Result<Guid>.Fail("Gönderen veya alıcı bulunamadı.");

            var note = new Note
            {
                Id = Guid.NewGuid(),
                SenderId = request.SenderId,
                ReceiverId = request.ReceiverId,
                Content = request.Content,
                SentAt = request.SentAt ?? DateTime.UtcNow
            };

            await _context.Notes.AddAsync(note, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Ok(note.Id, "Not başarıyla gönderildi.");
        }
    }
}
