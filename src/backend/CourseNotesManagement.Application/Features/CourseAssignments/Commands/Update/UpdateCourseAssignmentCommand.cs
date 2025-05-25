using CourseNotesManagement.Application.Common;
using MediatR;

namespace CourseNotesManagement.Application.Features.CourseAssignments.Commands.Update
{
    public class UpdateCourseAssignmentCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public Guid TeacherId { get; set; }
        public Guid CourseId { get; set; }
        public DateTime AssignmentDate { get; set; }
    }
}
