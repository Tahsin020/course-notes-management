using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CourseNotesManagement.Application.Features.CourseAssignments.Queries.GetCourseAssignmentById
{
    public class GetCourseAssignmentByIdQueryHandler : IRequestHandler<GetCourseAssignmentByIdQuery, Result<CourseAssignmentDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetCourseAssignmentByIdQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<CourseAssignmentDto>> Handle(GetCourseAssignmentByIdQuery request, CancellationToken cancellationToken)
        {
            var assignment = await _context.CourseAssignments
                .Include(a => a.Teacher)
                .Include(a => a.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

            if (assignment == null)
                return Result<CourseAssignmentDto>.Fail("Atama bulunamadı.");

            var dto = new CourseAssignmentDto
            {
                Id = assignment.Id,
                TeacherId = assignment.TeacherId,
                TeacherName = assignment.Teacher.FirstName + " " + assignment.Teacher.LastName,
                CourseId = assignment.CourseId,
                CourseName = assignment.Course.Name,
                AssignmentDate = assignment.AssignmentDate
            };

            return Result<CourseAssignmentDto>.Ok(dto);
        }
    }
}
