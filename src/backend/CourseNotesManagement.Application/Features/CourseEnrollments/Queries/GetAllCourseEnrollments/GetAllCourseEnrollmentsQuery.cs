using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Application.Features.CourseEnrollments.Queries.GetCourseEnrollmentById;
using MediatR;

namespace CourseNotesManagement.Application.Features.CourseEnrollments.Queries.GetAllCourseEnrollments
{
    public class GetAllCourseEnrollmentsQuery : IRequest<Result<List<CourseEnrollmentDto>>>
    {
    }
}
