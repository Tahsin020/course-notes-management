using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Application.Features.Parents.Queries.GetParentById;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CourseNotesManagement.Application.Features.Parents.Queries.GetAllParents
{
    public class GetAllParentsQueryHandler : IRequestHandler<GetAllParentsQuery, Result<List<ParentDto>>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllParentsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<ParentDto>>> Handle(GetAllParentsQuery request, CancellationToken cancellationToken)
        {
            var parents = await _context.Parents
                .Include(p => p.Children)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var result = parents.Select(parent => new ParentDto
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
            }).ToList();

            return Result<List<ParentDto>>.Ok(result);
        }
    }
}
