using CourseNotesManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseNotesManagement.Infrastructure.Configurations;

public class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.HasKey(n => n.Id);

        builder.Property(n => n.Content).IsRequired().HasMaxLength(1000);
        builder.Property(n => n.SentAt).IsRequired();

        builder.HasOne(n => n.Sender)
               .WithMany()
               .HasForeignKey(n => n.SenderId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(n => n.Receiver)
               .WithMany()
               .HasForeignKey(n => n.ReceiverId)
               .OnDelete(DeleteBehavior.Restrict);

        // BaseEntity alanları
        builder.Property(n => n.CreatedAt).IsRequired();
        builder.Property(n => n.UpdatedAt);
        builder.Property(n => n.CreatedBy);
        builder.Property(n => n.UpdatedBy);
    }
}