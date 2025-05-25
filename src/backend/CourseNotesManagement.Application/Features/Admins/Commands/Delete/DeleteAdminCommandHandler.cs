using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CourseNotesManagement.Application.Features.Admins.Commands.Delete
{
    public class DeleteAdminCommandHandler : IRequestHandler<DeleteAdminCommand, Result<Guid>>
    {
        private readonly ApplicationDbContext _context;

        public DeleteAdminCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Guid>> Handle(DeleteAdminCommand request, CancellationToken cancellationToken)
        {
            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

            if (admin == null)
                return Result<Guid>.Fail("Admin bulunamadı.");

            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Ok(admin.Id, "Admin başarıyla silindi.");
        }
    }
}
