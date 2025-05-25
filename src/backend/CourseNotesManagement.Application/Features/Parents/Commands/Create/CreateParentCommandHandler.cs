using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Domain.Entities;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CourseNotesManagement.Application.Features.Parents.Commands.Create;

public class CreateParentCommandHandler : IRequestHandler<CreateParentCommand, Result<Guid>>
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHasher<Parent> _passwordHasher;

    public CreateParentCommandHandler(ApplicationDbContext context, IPasswordHasher<Parent> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<Guid>> Handle(CreateParentCommand request, CancellationToken cancellationToken)
    {
        if (await _context.Parents.AnyAsync(p => p.Email == request.Email, cancellationToken))
            return Result<Guid>.Fail("Bu e-posta zaten kayıtlı.");

        var parent = new Parent
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            TcNo = request.TcNo,
            PhoneNumber = request.PhoneNumber,
            Role = "Parent"
        };

        parent.PasswordHash = _passwordHasher.HashPassword(parent, request.Password);

        _context.Parents.Add(parent);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Ok(parent.Id, "Veli başarıyla oluşturuldu.");
    }
}