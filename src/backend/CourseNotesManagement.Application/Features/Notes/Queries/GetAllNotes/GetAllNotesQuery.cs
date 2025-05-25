using CourseNotesManagement.Application.Common;
using CourseNotesManagement.Application.Features.Notes.Queries.GetNoteById;
using MediatR;

namespace CourseNotesManagement.Application.Features.Notes.Queries.GetAllNotes
{
    public class GetAllNotesQuery : IRequest<Result<List<NoteDto>>>
    {
    }
}
