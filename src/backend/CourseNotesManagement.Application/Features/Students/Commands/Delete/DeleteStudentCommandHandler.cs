using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CourseNotesManagement.Application.Features.Students.Commands.Delete
{
    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, Result<Guid>>
    {
        private readonly ApplicationDbContext _context;

        public DeleteStudentCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Guid>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

            if (student == null)
                return Result<Guid>.Fail("Öğrenci bulunamadı.");

            _context.Students.Remove(student);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Ok(student.Id, "Öğrenci başarıyla silindi.");
        }
    }
}
