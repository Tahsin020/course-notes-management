using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;

namespace CourseNotesManagement.Application.Features.Notes.Commands.Update
{
    public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, Result<Guid>>
    {
        private readonly ApplicationDbContext _context;

        public UpdateNoteCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Guid>> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await _context.Notes.FindAsync(new object[] { request.Id }, cancellationToken);

            if (note == null)
                return Result<Guid>.Fail("Not bulunamadı.");

            note.Content = request.Content;

            _context.Notes.Update(note);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Ok(note.Id, "Not başarıyla güncellendi.");
        }
    }
}
