using CourseNotesManagement.Application.Common;
using MediatR;

namespace CourseNotesManagement.Application.Features.CourseEnrollments.Queries.GetCourseEnrollmentById
{
    public class GetCourseEnrollmentByIdQuery : IRequest<Result<CourseEnrollmentDto>>
    {
        public Guid Id { get; set; }
        public GetCourseEnrollmentByIdQuery(Guid id) => Id = id;
    }
}
