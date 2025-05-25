using CourseNotesManagement.Application.Common;
using MediatR;

namespace CourseNotesManagement.Application.Features.Teachers.Commands.Delete
{
    public class DeleteTeacherCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public DeleteTeacherCommand(Guid id)
        {
            Id = id;
        }
    }
}
