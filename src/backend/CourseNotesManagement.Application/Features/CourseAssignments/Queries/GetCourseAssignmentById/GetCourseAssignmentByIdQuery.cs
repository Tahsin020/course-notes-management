using CourseNotesManagement.Application.Common;
using MediatR;

namespace CourseNotesManagement.Application.Features.CourseAssignments.Queries.GetCourseAssignmentById
{
    public class GetCourseAssignmentByIdQuery : IRequest<Result<CourseAssignmentDto>>
    {
        public Guid Id { get; set; }
        public GetCourseAssignmentByIdQuery(Guid id) => Id = id;
    }
}
