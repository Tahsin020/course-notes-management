using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Application.Features.Teachers.Queries.GetTeacherById;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetTeacherByIdQueryHandler : IRequestHandler<GetTeacherByIdQuery, Result<TeacherDto>>
{
    private readonly ApplicationDbContext _context;

    public GetTeacherByIdQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<TeacherDto>> Handle(GetTeacherByIdQuery request, CancellationToken cancellationToken)
    {
        var teacher = await _context.Teachers
            .Include(t => t.CourseAssignments)
                .ThenInclude(ca => ca.Course)
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (teacher == null)
            return Result<TeacherDto>.Fail("Öğretmen bulunamadı.");

        var dto = new TeacherDto
        {
            Id = teacher.Id,
            FirstName = teacher.FirstName,
            LastName = teacher.LastName,
            Email = teacher.Email,
            PasswordHash = teacher.PasswordHash,
            TcNo = teacher.TcNo,
            PhoneNumber = teacher.PhoneNumber,
            Role = teacher.Role,
            CourseAssignments = teacher.CourseAssignments.Select(ca => new CourseAssignmentDto
            {
                Id = ca.Id,
                CourseId = ca.CourseId,
                CourseName = ca.Course?.Name,
                AssignmentDate = ca.AssignmentDate
            }).ToList()
        };

        return Result<TeacherDto>.Ok(dto);
    }
}
