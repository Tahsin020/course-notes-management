using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CourseNotesManagement.Application.Features.CourseEnrollments.Commands.Update
{
    public class UpdateCourseEnrollmentCommandHandler : IRequestHandler<UpdateCourseEnrollmentCommand, Result<Guid>>
    {
        private readonly ApplicationDbContext _context;

        public UpdateCourseEnrollmentCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Guid>> Handle(UpdateCourseEnrollmentCommand request, CancellationToken cancellationToken)
        {
            var enrollment = await _context.CourseEnrollments.FindAsync(new object[] { request.Id }, cancellationToken);

            if (enrollment == null)
                return Result<Guid>.Fail("Kayıt bulunamadı.");

            // Duplicate kontrolü
            bool duplicate = await _context.CourseEnrollments.AnyAsync(
                ce => ce.StudentId == request.StudentId &&
                      ce.CourseId == request.CourseId &&
                      ce.Id != request.Id,
                cancellationToken);

            if (duplicate)
                return Result<Guid>.Fail("Bu öğrenci bu derse zaten kayıtlı.");

            var studentExists = await _context.Students.AnyAsync(s => s.Id == request.StudentId, cancellationToken);
            var courseExists = await _context.Courses.AnyAsync(c => c.Id == request.CourseId, cancellationToken);
            if (!studentExists || !courseExists)
                return Result<Guid>.Fail("Öğrenci veya ders bulunamadı.");

            enrollment.StudentId = request.StudentId;
            enrollment.CourseId = request.CourseId;
            enrollment.EnrollmentDate = request.EnrollmentDate;

            _context.CourseEnrollments.Update(enrollment);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Ok(enrollment.Id, "Kayıt başarıyla güncellendi.");
        }
    }
}
