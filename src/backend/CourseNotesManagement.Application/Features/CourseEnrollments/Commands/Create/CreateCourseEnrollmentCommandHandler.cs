using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Domain.Entities;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CourseNotesManagement.Application.Features.CourseEnrollments.Commands.Create
{
    public class CreateCourseEnrollmentCommandHandler : IRequestHandler<CreateCourseEnrollmentCommand, Result<Guid>>
    {
        private readonly ApplicationDbContext _context;

        public CreateCourseEnrollmentCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Guid>> Handle(CreateCourseEnrollmentCommand request, CancellationToken cancellationToken)
        {
            // Aynı öğrenci, aynı derse birden fazla kez kaydolamasın
            bool exists = await _context.CourseEnrollments.AnyAsync(
                ce => ce.StudentId == request.StudentId && ce.CourseId == request.CourseId,
                cancellationToken);

            if (exists)
                return Result<Guid>.Fail("Bu öğrenci bu derse zaten kayıtlı.");

            // Öğrenci ve Kurs var mı kontrolü
            var studentExists = await _context.Students.AnyAsync(s => s.Id == request.StudentId, cancellationToken);
            var courseExists = await _context.Courses.AnyAsync(c => c.Id == request.CourseId, cancellationToken);
            if (!studentExists || !courseExists)
                return Result<Guid>.Fail("Öğrenci veya ders bulunamadı.");

            var enrollment = new CourseEnrollment
            {
                Id = Guid.NewGuid(),
                StudentId = request.StudentId,
                CourseId = request.CourseId,
                EnrollmentDate = request.EnrollmentDate
            };

            await _context.CourseEnrollments.AddAsync(enrollment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Ok(enrollment.Id, "Derse kayıt başarıyla yapıldı.");
        }
    }
}
