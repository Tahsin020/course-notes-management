using CourseNotesManagement.Application.Common;
using MediatR;

namespace CourseNotesManagement.Application.Features.Teachers.Queries.GetTeacherById
{
    public class GetTeacherByIdQuery : IRequest<Result<TeacherDto>>
    {
        public Guid Id { get; set; }

        public GetTeacherByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}