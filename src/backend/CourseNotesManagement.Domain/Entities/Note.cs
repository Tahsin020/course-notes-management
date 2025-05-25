using CourseNotesManagement.Domain.Common;

namespace CourseNotesManagement.Domain.Entities;

public class Note : BaseEntity
{
    public Guid SenderId { get; set; }
    public User Sender { get; set; } = default!;
    public Guid ReceiverId { get; set; }
    public User Receiver { get; set; } = default!;
    public string Content { get; set; } = default!;
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
}