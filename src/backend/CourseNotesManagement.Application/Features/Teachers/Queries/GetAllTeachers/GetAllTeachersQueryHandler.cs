using AutoMapper;
using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Application.Features.Teachers.Queries.GetTeacherById;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CourseNotesManagement.Application.Features.Teachers.Queries.GetAllTeachers
{
    public class GetAllTeachersQueryHandler : IRequestHandler<GetAllTeachersQuery, Result<List<TeacherDto>>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllTeachersQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<List<TeacherDto>>> Handle(GetAllTeachersQuery request, CancellationToken cancellationToken)
        {
            var teachers = await _context.Teachers
                .Include(t => t.CourseAssignments)
                    .ThenInclude(ca => ca.Course)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var dtoList = _mapper.Map<List<TeacherDto>>(teachers);

            return Result<List<TeacherDto>>.Ok(dtoList);
        }
    }
}