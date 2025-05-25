using CourseNotesManagement.Application.Common;
using MediatR;

namespace CourseNotesManagement.Application.Features.Students.Commands.Delete
{
    public class DeleteStudentCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public DeleteStudentCommand(Guid id)
        {
            Id = id;
        }
    }
}
