using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CourseNotesManagement.Application.Features.Courses.Queries.GetAllCourses
{
    public class GetAllCoursesQueryHandler : IRequestHandler<GetAllCoursesQuery, Result<List<CourseDto>>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllCoursesQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<CourseDto>>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            var courses = await _context.Courses
                .Include(c => c.CourseAssignments)
                .Include(c => c.CourseEnrollments)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var result = courses.Select(course => new CourseDto
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                AssignmentCount = course.CourseAssignments.Count,
                EnrollmentCount = course.CourseEnrollments.Count
            }).ToList();

            return Result<List<CourseDto>>.Ok(result);
        }
    }
}
