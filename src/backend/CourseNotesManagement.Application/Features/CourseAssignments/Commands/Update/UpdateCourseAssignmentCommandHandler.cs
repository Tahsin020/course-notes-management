using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CourseNotesManagement.Application.Features.CourseAssignments.Commands.Update
{
    public class UpdateCourseAssignmentCommandHandler : IRequestHandler<UpdateCourseAssignmentCommand, Result<Guid>>
    {
        private readonly ApplicationDbContext _context;

        public UpdateCourseAssignmentCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Guid>> Handle(UpdateCourseAssignmentCommand request, CancellationToken cancellationToken)
        {
            var assignment = await _context.CourseAssignments.FindAsync(new object[] { request.Id }, cancellationToken);

            if (assignment == null)
                return Result<Guid>.Fail("Atama bulunamadı.");

            // Yeni atama varsa ve aynı teacher-course ise, hata döndür
            bool duplicate = await _context.CourseAssignments.AnyAsync(
                ca => ca.TeacherId == request.TeacherId &&
                      ca.CourseId == request.CourseId &&
                      ca.Id != request.Id,
                cancellationToken);

            if (duplicate)
                return Result<Guid>.Fail("Bu öğretmen bu derse zaten atanmış.");

            var teacherExists = await _context.Teachers.AnyAsync(t => t.Id == request.TeacherId, cancellationToken);
            var courseExists = await _context.Courses.AnyAsync(c => c.Id == request.CourseId, cancellationToken);
            if (!teacherExists || !courseExists)
                return Result<Guid>.Fail("Öğretmen veya ders bulunamadı.");

            assignment.TeacherId = request.TeacherId;
            assignment.CourseId = request.CourseId;
            assignment.AssignmentDate = request.AssignmentDate;

            _context.CourseAssignments.Update(assignment);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Ok(assignment.Id, "Atama başarıyla güncellendi.");
        }
    }
}