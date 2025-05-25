using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Domain.Entities;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CourseNotesManagement.Application.Features.Admins.Commands.Update
{
    public class UpdateAdminCommandHandler : IRequestHandler<UpdateAdminCommand, Result<Guid>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<Admin> _passwordHasher;

        public UpdateAdminCommandHandler(ApplicationDbContext context, IPasswordHasher<Admin> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result<Guid>> Handle(UpdateAdminCommand request, CancellationToken cancellationToken)
        {
            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

            if (admin == null)
                return Result<Guid>.Fail("Admin bulunamadı.");

            if (admin.Email != request.Email &&
                await _context.Admins.AnyAsync(a => a.Email == request.Email && a.Id != request.Id, cancellationToken))
            {
                return Result<Guid>.Fail("Bu e-posta başka bir adminde zaten kullanılıyor.");
            }

            admin.FirstName = request.FirstName;
            admin.LastName = request.LastName;
            admin.Email = request.Email;
            admin.TcNo = request.TcNo;
            admin.PhoneNumber = request.PhoneNumber;
            admin.Role = "Admin";

            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                admin.PasswordHash = _passwordHasher.HashPassword(admin, request.Password);
            }

            _context.Admins.Update(admin);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Ok(admin.Id, "Admin başarıyla güncellendi.");
        }
    }
}