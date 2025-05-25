using CourseNotesManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseNotesManagement.Infrastructure.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Description).HasMaxLength(500);

        builder.HasMany(c => c.CourseAssignments)
               .WithOne(ca => ca.Course)
               .HasForeignKey(ca => ca.CourseId);

        builder.HasMany(c => c.CourseEnrollments)
               .WithOne(ce => ce.Course)
               .HasForeignKey(ce => ce.CourseId);

        // BaseEntity alanları
        builder.Property(c => c.CreatedAt).IsRequired();
        builder.Property(c => c.UpdatedAt);
        builder.Property(c => c.CreatedBy);
        builder.Property(c => c.UpdatedBy);
    }
}
