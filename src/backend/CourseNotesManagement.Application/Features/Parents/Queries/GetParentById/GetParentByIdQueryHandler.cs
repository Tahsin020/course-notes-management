using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CourseNotesManagement.Application.Features.Parents.Queries.GetParentById
{
    public class GetParentByIdQueryHandler : IRequestHandler<GetParentByIdQuery, Result<ParentDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetParentByIdQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<ParentDto>> Handle(GetParentByIdQuery request, CancellationToken cancellationToken)
        {
            var parent = await _context.Parents
                .Include(p => p.Children)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (parent == null)
                return Result<ParentDto>.Fail("Veli bulunamadı.");

            var dto = new ParentDto
            {
                Id = parent.Id,
                FirstName = parent.FirstName,
                LastName = parent.LastName,
                Email = parent.Email,
                TcNo = parent.TcNo,
                PhoneNumber = parent.PhoneNumber,
                Children = parent.Children.Select(s => new ChildStudentDto
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName
                }).ToList()
            };

            return Result<ParentDto>.Ok(dto);
        }
    }
}
