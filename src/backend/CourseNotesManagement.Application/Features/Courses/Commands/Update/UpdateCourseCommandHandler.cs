using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CourseNotesManagement.Application.Features.Courses.Commands.Update
{
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, Result<Guid>>
    {
        private readonly ApplicationDbContext _context;

        public UpdateCourseCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Guid>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _context.Courses.FindAsync(new object[] { request.Id }, cancellationToken);

            if (course == null)
                return Result<Guid>.Fail("Kurs bulunamadı.");

            if (course.Name != request.Name &&
                await _context.Courses.AnyAsync(c => c.Name == request.Name && c.Id != request.Id, cancellationToken))
                return Result<Guid>.Fail("Bu isimde başka bir kurs zaten var.");

            course.Name = request.Name;
            course.Description = request.Description;

            _context.Courses.Update(course);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Ok(course.Id, "Kurs başarıyla güncellendi.");
        }
    }

}
