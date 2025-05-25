using CourseNotesManagement.Application.Common;
using MediatR;

namespace CourseNotesManagement.Application.Features.CourseAssignments.Commands.Delete
{
    public class DeleteCourseAssignmentCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public DeleteCourseAssignmentCommand(Guid id) => Id = id;
    }
}
