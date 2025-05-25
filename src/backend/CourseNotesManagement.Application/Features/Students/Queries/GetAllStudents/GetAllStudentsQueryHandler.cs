using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Application.Features.Students.Queries.GetStudentById;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, Result<List<StudentDto>>>
{
    private readonly ApplicationDbContext _context;

    public GetAllStudentsQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<StudentDto>>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        var students = await _context.Students
            .Include(s => s.Parent)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var result = students.Select(student => new StudentDto
        {
            Id = student.Id,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Email = student.Email,
            TcNo = student.TcNo,
            PhoneNumber = student.PhoneNumber,
            ParentId = student.ParentId,
            ParentFullName = student.Parent != null ? $"{student.Parent.FirstName} {student.Parent.LastName}" : null
        }).ToList();

        return Result<List<StudentDto>>.Ok(result);
    }
}