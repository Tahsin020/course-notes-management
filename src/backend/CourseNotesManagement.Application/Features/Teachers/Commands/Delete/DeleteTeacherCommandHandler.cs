using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CourseNotesManagement.Application.Features.Teachers.Commands.Delete
{
    public class DeleteTeacherCommandHandler : IRequestHandler<DeleteTeacherCommand, Result<Guid>>
    {
        private readonly ApplicationDbContext _context;

        public DeleteTeacherCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Guid>> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            if (teacher == null)
                return Result<Guid>.Fail("Öğretmen bulunamadı.");

            // Eğer ilişkili CourseAssignment kayıtları da silinmeli ise, burada ekleyebilirsin:
            var assignments = await _context.CourseAssignments.Where(ca => ca.TeacherId == teacher.Id).ToListAsync(cancellationToken);
            _context.CourseAssignments.RemoveRange(assignments);

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Ok(teacher.Id, "Öğretmen başarıyla silindi.");
        }
    }
}
