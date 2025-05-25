using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Domain.Entities;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CourseNotesManagement.Application.Features.Teachers.Commands.Update;

public class UpdateTeacherCommandHandler : IRequestHandler<UpdateTeacherCommand, Result<Guid>>
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHasher<Teacher> _passwordHasher;

    public UpdateTeacherCommandHandler(ApplicationDbContext context, IPasswordHasher<Teacher> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<Guid>> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
    {
        var teacher = await _context.Teachers
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (teacher == null)
            return Result<Guid>.Fail("Öğretmen bulunamadı.");

        // E-posta başka bir öğretmende var mı kontrolü (güncellenirse)
        if (teacher.Email != request.Email &&
            await _context.Teachers.AnyAsync(t => t.Email == request.Email && t.Id != request.Id, cancellationToken))
        {
            return Result<Guid>.Fail("Bu e-posta başka bir öğretmende zaten kullanılıyor.");
        }

        // Alan güncellemeleri
        teacher.FirstName = request.FirstName;
        teacher.LastName = request.LastName;
        teacher.Email = request.Email;
        teacher.TcNo = request.TcNo;
        teacher.PhoneNumber = request.PhoneNumber;
        teacher.Role = "Teacher"; // Güvenlik için!

        // Şifre değiştirilecekse hashle
        if (!string.IsNullOrWhiteSpace(request.Password))
        {
            teacher.PasswordHash = _passwordHasher.HashPassword(teacher, request.Password);
        }

        _context.Teachers.Update(teacher);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Ok(teacher.Id, "Öğretmen başarıyla güncellendi.");
    }
}