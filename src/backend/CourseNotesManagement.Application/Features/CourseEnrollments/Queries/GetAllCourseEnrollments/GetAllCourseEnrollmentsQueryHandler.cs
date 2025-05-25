using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Application.Features.CourseEnrollments.Queries.GetCourseEnrollmentById;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CourseNotesManagement.Application.Features.CourseEnrollments.Queries.GetAllCourseEnrollments
{
    public class GetAllCourseEnrollmentsQueryHandler : IRequestHandler<GetAllCourseEnrollmentsQuery, Result<List<CourseEnrollmentDto>>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllCourseEnrollmentsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<CourseEnrollmentDto>>> Handle(GetAllCourseEnrollmentsQuery request, CancellationToken cancellationToken)
        {
            var enrollments = await _context.CourseEnrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var result = enrollments.Select(e => new CourseEnrollmentDto
            {
                Id = e.Id,
                StudentId = e.StudentId,
                StudentName = e.Student.FirstName + " " + e.Student.LastName,
                CourseId = e.CourseId,
                CourseName = e.Course.Name,
                EnrollmentDate = e.EnrollmentDate
            }).ToList();

            return Result<List<CourseEnrollmentDto>>.Ok(result);
        }
    }
}
