using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CourseNotesManagement.Application.Features.CourseEnrollments.Queries.GetCourseEnrollmentById
{
    public class GetCourseEnrollmentByIdQueryHandler : IRequestHandler<GetCourseEnrollmentByIdQuery, Result<CourseEnrollmentDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetCourseEnrollmentByIdQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<CourseEnrollmentDto>> Handle(GetCourseEnrollmentByIdQuery request, CancellationToken cancellationToken)
        {
            var enrollment = await _context.CourseEnrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (enrollment == null)
                return Result<CourseEnrollmentDto>.Fail("Kayıt bulunamadı.");

            var dto = new CourseEnrollmentDto
            {
                Id = enrollment.Id,
                StudentId = enrollment.StudentId,
                StudentName = enrollment.Student.FirstName + " " + enrollment.Student.LastName,
                CourseId = enrollment.CourseId,
                CourseName = enrollment.Course.Name,
                EnrollmentDate = enrollment.EnrollmentDate
            };

            return Result<CourseEnrollmentDto>.Ok(dto);
        }
    }
}
