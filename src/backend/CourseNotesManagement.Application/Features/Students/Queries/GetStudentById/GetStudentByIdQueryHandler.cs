using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Application.Features.Students.Queries.GetStudentById;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, Result<StudentDto>>
{
    private readonly ApplicationDbContext _context;

    public GetStudentByIdQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<StudentDto>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        var student = await _context.Students
            .Include(s => s.Parent)
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

        if (student == null)
            return Result<StudentDto>.Fail("Öğrenci bulunamadı.");

        // Şifre gibi gizli alanları DTO'ya taşıma!
        var dto = new StudentDto
        {
            Id = student.Id,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Email = student.Email,
            TcNo = student.TcNo,
            PhoneNumber = student.PhoneNumber,
            ParentId = student.ParentId,
            ParentFullName = student.Parent != null ? $"{student.Parent.FirstName} {student.Parent.LastName}" : null
        };

        return Result<StudentDto>.Ok(dto);
    }
}
