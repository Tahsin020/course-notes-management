using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Domain.Entities;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CourseNotesManagement.Application.Features.Parents.Commands.Update
{
    public class UpdateParentCommandHandler : IRequestHandler<UpdateParentCommand, Result<Guid>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<Parent> _passwordHasher;

        public UpdateParentCommandHandler(ApplicationDbContext context, IPasswordHasher<Parent> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result<Guid>> Handle(UpdateParentCommand request, CancellationToken cancellationToken)
        {
            var parent = await _context.Parents.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (parent == null)
                return Result<Guid>.Fail("Veli bulunamadı.");

            if (parent.Email != request.Email &&
                await _context.Parents.AnyAsync(p => p.Email == request.Email && p.Id != request.Id, cancellationToken))
            {
                return Result<Guid>.Fail("Bu e-posta başka bir velide zaten kullanılıyor.");
            }

            parent.FirstName = request.FirstName;
            parent.LastName = request.LastName;
            parent.Email = request.Email;
            parent.TcNo = request.TcNo;
            parent.PhoneNumber = request.PhoneNumber;
            parent.Role = "Parent";

            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                parent.PasswordHash = _passwordHasher.HashPassword(parent, request.Password);
            }

            _context.Parents.Update(parent);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Ok(parent.Id, "Veli başarıyla güncellendi.");
        }
    }
}
