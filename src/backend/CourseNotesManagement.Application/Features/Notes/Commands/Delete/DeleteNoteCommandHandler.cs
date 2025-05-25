using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;

namespace CourseNotesManagement.Application.Features.Notes.Commands.Delete
{
    public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand, Result<Guid>>
    {
        private readonly ApplicationDbContext _context;

        public DeleteNoteCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Guid>> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await _context.Notes.FindAsync(new object[] { request.Id }, cancellationToken);

            if (note == null)
                return Result<Guid>.Fail("Not bulunamadı.");

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Ok(note.Id, "Not başarıyla silindi.");
        }
    }
}
