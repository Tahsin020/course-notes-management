using CourseNotesManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseNotesManagement.Infrastructure.Configurations;

public class CourseAssignmentConfiguration : IEntityTypeConfiguration<CourseAssignment>
{
    public void Configure(EntityTypeBuilder<CourseAssignment> builder)
    {
        builder.HasKey(ca => ca.Id);
        builder.Property(ca => ca.AssignmentDate).IsRequired();

        builder.HasOne(ca => ca.Teacher)
               .WithMany(t => t.CourseAssignments)
               .HasForeignKey(ca => ca.TeacherId);

        builder.HasOne(ca => ca.Course)
               .WithMany(c => c.CourseAssignments)
               .HasForeignKey(ca => ca.CourseId);

        // BaseEntity alanları
        builder.Property(ca => ca.CreatedAt).IsRequired();
        builder.Property(ca => ca.UpdatedAt);
        builder.Property(ca => ca.CreatedBy);
        builder.Property(ca => ca.UpdatedBy);
    }
}
