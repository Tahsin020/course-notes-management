using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Domain.Entities;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CourseNotesManagement.Application.Features.Students.Commands.Update
{
    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, Result<Guid>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<Student> _passwordHasher;

        public UpdateStudentCommandHandler(ApplicationDbContext context, IPasswordHasher<Student> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result<Guid>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _context.Students
                .Include(s => s.Parent)
                .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

            if (student == null)
                return Result<Guid>.Fail("Öğrenci bulunamadı.");

            // E-posta başka bir öğrenciye ait mi?
            if (student.Email != request.Email &&
                await _context.Students.AnyAsync(s => s.Email == request.Email && s.Id != request.Id, cancellationToken))
            {
                return Result<Guid>.Fail("Bu e-posta başka bir öğrenciye ait.");
            }

            // ParentId güncellemesi varsa kontrol et
            if (request.ParentId != student.ParentId)
            {
                if (request.ParentId != null)
                {
                    var parent = await _context.Parents.FindAsync(new object[] { request.ParentId.Value }, cancellationToken);
                    if (parent == null)
                        return Result<Guid>.Fail("Veli bulunamadı.");
                    student.ParentId = request.ParentId;
                    student.Parent = parent;
                }
                else
                {
                    student.ParentId = null;
                    student.Parent = null;
                }
            }

            student.FirstName = request.FirstName;
            student.LastName = request.LastName;
            student.Email = request.Email;
            student.TcNo = request.TcNo;
            student.PhoneNumber = request.PhoneNumber;
            student.Role = "Student"; // Güvenlik için!

            // Şifre güncelleme
            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                student.PasswordHash = _passwordHasher.HashPassword(student, request.Password);
            }

            _context.Students.Update(student);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Ok(student.Id, "Öğrenci başarıyla güncellendi.");
        }
    }
}
