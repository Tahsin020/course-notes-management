using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Domain.Entities;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CourseNotesManagement.Application.Features.Admins.Commands.Create;
public class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand, Result<Guid>>
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHasher<Admin> _passwordHasher;

    public CreateAdminCommandHandler(ApplicationDbContext context, IPasswordHasher<Admin> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<Guid>> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
    {
        if (await _context.Admins.AnyAsync(a => a.Email == request.Email, cancellationToken))
            return Result<Guid>.Fail("Bu e-posta zaten kayıtlı.");

        var admin = new Admin
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            TcNo = request.TcNo,
            PhoneNumber = request.PhoneNumber,
            Role = "Admin"
        };

        admin.PasswordHash = _passwordHasher.HashPassword(admin, request.Password);

        _context.Admins.Add(admin);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Ok(admin.Id, "Admin başarıyla oluşturuldu.");
    }
}
