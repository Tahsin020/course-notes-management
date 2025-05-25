using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Application.Features.Students.Queries.GetStudentById;
using MediatR;

public class GetAllStudentsQuery : IRequest<Result<List<StudentDto>>>
{
}