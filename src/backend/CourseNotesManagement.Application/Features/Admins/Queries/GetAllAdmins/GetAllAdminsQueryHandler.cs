using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Application.Features.Admins.Queries.GetAllAdmins;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static CourseNotesManagement.Application.Features.Admins.Queries.GetAdminById.GetAllAdminsQueryHandler;

namespace CourseNotesManagement.Application.Features.Admins.Queries.GetAdminById;

public partial class GetAllAdminsQueryHandler : IRequestHandler<GetAllAdminsQuery, Result<List<AdminDto>>>
{
    private readonly ApplicationDbContext _context;

    public GetAllAdminsQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<AdminDto>>> Handle(GetAllAdminsQuery request, CancellationToken cancellationToken)
    {
        var admins = await _context.Admins.AsNoTracking().ToListAsync(cancellationToken);

        var result = admins.Select(admin => new AdminDto
        {
            Id = admin.Id,
            FirstName = admin.FirstName,
            LastName = admin.LastName,
            Email = admin.Email,
            TcNo = admin.TcNo,
            PhoneNumber = admin.PhoneNumber
        }).ToList();

        return Result<List<AdminDto>>.Ok(result);
    }
}
