using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Infrastructure.Persistence;
using MediatR;

namespace CourseNotesManagement.Application.Features.CourseAssignments.Commands.Delete
{
    public class DeleteCourseAssignmentCommandHandler : IRequestHandler<DeleteCourseAssignmentCommand, Result<Guid>>
    {
        private readonly ApplicationDbContext _context;

        public DeleteCourseAssignmentCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Guid>> Handle(DeleteCourseAssignmentCommand request, CancellationToken cancellationToken)
        {
            var assignment = await _context.CourseAssignments.FindAsync(new object[] { request.Id }, cancellationToken);

            if (assignment == null)
                return Result<Guid>.Fail("Atama bulunamadı.");

            _context.CourseAssignments.Remove(assignment);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Ok(assignment.Id, "Atama başarıyla silindi.");
        }
    }
}
