using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Domain.Entities;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CourseNotesManagement.Application.Features.CourseAssignments.Commands.Create
{
    public class CreateCourseAssignmentCommandHandler : IRequestHandler<CreateCourseAssignmentCommand, Result<Guid>>
    {
        private readonly ApplicationDbContext _context;

        public CreateCourseAssignmentCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Guid>> Handle(CreateCourseAssignmentCommand request, CancellationToken cancellationToken)
        {
            // Aynı öğretmen aynı dersi birden fazla kez atamasın
            bool exists = await _context.CourseAssignments.AnyAsync(
                ca => ca.TeacherId == request.TeacherId && ca.CourseId == request.CourseId,
                cancellationToken);

            if (exists)
                return Result<Guid>.Fail("Bu öğretmen bu derse zaten atanmış.");

            // Teacher ve Course var mı kontrolü
            var teacherExists = await _context.Teachers.AnyAsync(t => t.Id == request.TeacherId, cancellationToken);
            var courseExists = await _context.Courses.AnyAsync(c => c.Id == request.CourseId, cancellationToken);
            if (!teacherExists || !courseExists)
                return Result<Guid>.Fail("Öğretmen veya ders bulunamadı.");

            var assignment = new CourseAssignment
            {
                Id = Guid.NewGuid(),
                TeacherId = request.TeacherId,
                CourseId = request.CourseId,
                AssignmentDate = request.AssignmentDate
            };

            await _context.CourseAssignments.AddAsync(assignment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Ok(assignment.Id, "Ders ataması başarıyla yapıldı.");
        }
    }
}
