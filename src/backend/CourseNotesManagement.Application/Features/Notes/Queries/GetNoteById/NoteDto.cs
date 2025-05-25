namespace CourseNotesManagement.Application.Features.Notes.Queries.GetNoteById
{
    public class NoteDto
    {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public string SenderName { get; set; } = default!;
        public Guid ReceiverId { get; set; }
        public string ReceiverName { get; set; } = default!;
        public string Content { get; set; } = default!;
        public DateTime SentAt { get; set; }
    }
}
