using CourseNotesManagement.Application.Common;
using MediatR;

namespace CourseNotesManagement.Application.Features.CourseEnrollments.Commands.Update
{
    public class UpdateCourseEnrollmentCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}
