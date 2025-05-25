using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Domain.Entities;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CourseNotesManagement.Application.Features.Students.Commands.Create;

public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Result<Guid>>
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHasher<Student> _passwordHasher;

    public CreateStudentCommandHandler(ApplicationDbContext context, IPasswordHasher<Student> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<Guid>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        // E-posta benzersiz mi kontrolü
        if (await _context.Students.AnyAsync(s => s.Email == request.Email, cancellationToken))
            return Result<Guid>.Fail("Bu e-posta zaten kayıtlı.");

        // ParentId kontrolü (isteğe bağlı)
        Parent? parent = null;
        if (request.ParentId.HasValue)
        {
            parent = await _context.Parents.FindAsync(new object[] { request.ParentId.Value }, cancellationToken);
            if (parent == null)
                return Result<Guid>.Fail("Veli bulunamadı.");
        }

        var student = new Student
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            TcNo = request.TcNo,
            PhoneNumber = request.PhoneNumber,
            ParentId = request.ParentId,
            Parent = parent,
            Role = "Student"
        };

        student.PasswordHash = _passwordHasher.HashPassword(student, request.Password);

        _context.Students.Add(student);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Ok(student.Id, "Öğrenci başarıyla oluşturuldu.");
    }
}