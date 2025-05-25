using CourseNotesManagement.Application.Common;
using MediatR;

namespace CourseNotesManagement.Application.Features.Courses.Commands.Delete
{
    public class DeleteCourseCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public DeleteCourseCommand(Guid id) => Id = id;
    }
}
