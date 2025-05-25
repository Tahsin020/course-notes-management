using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Application.Features.Teachers.Queries.GetTeacherById;
using MediatR;

namespace CourseNotesManagement.Application.Features.Teachers.Queries.GetAllTeachers
{
    public class GetAllTeachersQuery : IRequest<Result<List<TeacherDto>>>
    {
    }
}