using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Application.Features.CourseAssignments.Queries.GetCourseAssignmentById;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CourseNotesManagement.Application.Features.CourseAssignments.Queries.GetAllCourseAssignments
{
    public class GetAllCourseAssignmentsQueryHandler : IRequestHandler<GetAllCourseAssignmentsQuery, Result<List<CourseAssignmentDto>>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllCourseAssignmentsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<CourseAssignmentDto>>> Handle(GetAllCourseAssignmentsQuery request, CancellationToken cancellationToken)
        {
            var assignments = await _context.CourseAssignments
                .Include(a => a.Teacher)
                .Include(a => a.Course)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var result = assignments.Select(a => new CourseAssignmentDto
            {
                Id = a.Id,
                TeacherId = a.TeacherId,
                TeacherName = a.Teacher.FirstName + " " + a.Teacher.LastName,
                CourseId = a.CourseId,
                CourseName = a.Course.Name,
                AssignmentDate = a.AssignmentDate
            }).ToList();

            return Result<List<CourseAssignmentDto>>.Ok(result);
        }
    }
}
