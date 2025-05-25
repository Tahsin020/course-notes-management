using CourseNotesManagement.Application.Common;
using MediatR;

namespace CourseNotesManagement.Application.Features.Notes.Queries.GetNoteById
{
    public class GetNoteByIdQuery : IRequest<Result<NoteDto>>
    {
        public Guid Id { get; set; }
        public GetNoteByIdQuery(Guid id) => Id = id;
    }
}
