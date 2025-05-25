using CourseNotesManagement.Application.Common;
using MediatR;

namespace CourseNotesManagement.Application.Features.Students.Queries.GetStudentById
{
    public class GetStudentByIdQuery : IRequest<Result<StudentDto>>
    {
        public Guid Id { get; set; }
        public GetStudentByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
