using CourseNotesManagement.Application.Common;
using MediatR;

namespace CourseNotesManagement.Application.Features.CourseEnrollments.Commands.Delete
{
    public class DeleteCourseEnrollmentCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public DeleteCourseEnrollmentCommand(Guid id) => Id = id;
    }
}
