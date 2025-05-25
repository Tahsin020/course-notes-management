using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Application.Features.Courses.Queries.GetAllCourses;
using MediatR;

namespace CourseNotesManagement.Application.Features.Courses.Queries.GetCourseById
{
    public class GetCourseByIdQuery : IRequest<Result<CourseDto>>
    {
        public Guid Id { get; set; }
        public GetCourseByIdQuery(Guid id) => Id = id;
    }
}
