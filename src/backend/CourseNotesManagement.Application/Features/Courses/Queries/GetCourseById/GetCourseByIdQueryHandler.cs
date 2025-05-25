using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Application.Features.Courses.Queries.GetAllCourses;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CourseNotesManagement.Application.Features.Courses.Queries.GetCourseById
{
    public class GetCourseByIdQueryHandler : IRequestHandler<GetCourseByIdQuery, Result<CourseDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetCourseByIdQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<CourseDto>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var course = await _context.Courses
                .Include(c => c.CourseAssignments)
                .Include(c => c.CourseEnrollments)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (course == null)
                return Result<CourseDto>.Fail("Kurs bulunamadı.");

            var dto = new CourseDto
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                AssignmentCount = course.CourseAssignments.Count,
                EnrollmentCount = course.CourseEnrollments.Count
            };

            return Result<CourseDto>.Ok(dto);
        }
    }
}
