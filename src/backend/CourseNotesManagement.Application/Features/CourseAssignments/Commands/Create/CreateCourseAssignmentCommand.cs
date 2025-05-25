using CourseNotesManagement.Application.Common;
using MediatR;

namespace CourseNotesManagement.Application.Features.CourseAssignments.Commands.Create
{
    public class CreateCourseAssignmentCommand : IRequest<Result<Guid>>
    {
        public Guid TeacherId { get; set; }
        public Guid CourseId { get; set; }
        public DateTime AssignmentDate { get; set; }
    }
}
