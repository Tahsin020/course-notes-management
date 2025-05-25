using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Domain.Entities;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CourseNotesManagement.Application.Features.Teachers.Commands.Create
{
    public class CreateTeacherCommandHandler : IRequestHandler<CreateTeacherCommand, Result<Guid>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<Teacher> _passwordHasher;

        public CreateTeacherCommandHandler(ApplicationDbContext context, IPasswordHasher<Teacher> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result<Guid>> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
        {
            // E-posta benzersiz mi kontrolü
            if (await _context.Teachers.AnyAsync(t => t.Email == request.Email, cancellationToken))
                return Result<Guid>.Fail("Bu e-posta zaten kayıtlı.");

            var teacher = new Teacher
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                TcNo = request.TcNo,
                PhoneNumber = request.PhoneNumber,
                Role = "Teacher" // Sadece Teacher!
            };

            teacher.PasswordHash = _passwordHasher.HashPassword(teacher, request.Password);

            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Ok(teacher.Id, "Öğretmen başarıyla oluşturuldu.");
        }
    }
}