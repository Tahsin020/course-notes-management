using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;

namespace CourseNotesManagement.Application.Features.CourseEnrollments.Commands.Delete
{
    public class DeleteCourseEnrollmentCommandHandler : IRequestHandler<DeleteCourseEnrollmentCommand, Result<Guid>>
    {
        private readonly ApplicationDbContext _context;

        public DeleteCourseEnrollmentCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Guid>> Handle(DeleteCourseEnrollmentCommand request, CancellationToken cancellationToken)
        {
            var enrollment = await _context.CourseEnrollments.FindAsync(new object[] { request.Id }, cancellationToken);

            if (enrollment == null)
                return Result<Guid>.Fail("Kayıt bulunamadı.");

            _context.CourseEnrollments.Remove(enrollment);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Ok(enrollment.Id, "Kayıt başarıyla silindi.");
        }
    }
}
