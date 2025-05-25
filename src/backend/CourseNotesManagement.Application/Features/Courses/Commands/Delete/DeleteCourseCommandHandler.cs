using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;

namespace CourseNotesManagement.Application.Features.Courses.Commands.Delete
{
    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, Result<Guid>>
    {
        private readonly ApplicationDbContext _context;

        public DeleteCourseCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Guid>> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _context.Courses.FindAsync(new object[] { request.Id }, cancellationToken);

            if (course == null)
                return Result<Guid>.Fail("Kurs bulunamadı.");

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Ok(course.Id, "Kurs başarıyla silindi.");
        }
    }
}
