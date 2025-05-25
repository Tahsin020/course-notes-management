using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CourseNotesManagement.Application.Features.Parents.Commands.Delete
{
    public class DeleteParentCommandHandler : IRequestHandler<DeleteParentCommand, Result<Guid>>
    {
        private readonly ApplicationDbContext _context;

        public DeleteParentCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Guid>> Handle(DeleteParentCommand request, CancellationToken cancellationToken)
        {
            var parent = await _context.Parents.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (parent == null)
                return Result<Guid>.Fail("Veli bulunamadı.");

            _context.Parents.Remove(parent);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Ok(parent.Id, "Veli başarıyla silindi.");
        }
    }
}
