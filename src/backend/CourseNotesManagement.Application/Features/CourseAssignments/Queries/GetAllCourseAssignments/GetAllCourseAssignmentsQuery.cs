using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Application.Features.CourseAssignments.Queries.GetCourseAssignmentById;
using MediatR;

namespace CourseNotesManagement.Application.Features.CourseAssignments.Queries.GetAllCourseAssignments
{
    public class GetAllCourseAssignmentsQuery : IRequest<Result<List<CourseAssignmentDto>>>
    {
    }
}
