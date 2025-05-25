using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static CourseNotesManagement.Application.Features.Admins.Queries.GetAdminById.GetAllAdminsQueryHandler;

namespace CourseNotesManagement.Application.Features.Admins.Queries.GetAdminById
{
    public class GetAdminByIdQueryHandler : IRequestHandler<GetAdminByIdQuery, Result<AdminDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetAdminByIdQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<AdminDto>> Handle(GetAdminByIdQuery request, CancellationToken cancellationToken)
        {
            var admin = await _context.Admins.AsNoTracking().FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

            if (admin == null)
                return Result<AdminDto>.Fail("Admin bulunamadı.");

            var dto = new AdminDto
            {
                Id = admin.Id,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email,
                TcNo = admin.TcNo,
                PhoneNumber = admin.PhoneNumber
            };

            return Result<AdminDto>.Ok(dto);
        }
    }
}
