using CourseNotesManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseNotesManagement.Infrastructure.Configurations;

public class CourseEnrollmentConfiguration : IEntityTypeConfiguration<CourseEnrollment>
{
    public void Configure(EntityTypeBuilder<CourseEnrollment> builder)
    {
        builder.HasKey(ce => ce.Id);
        builder.Property(ce => ce.EnrollmentDate).IsRequired();

        builder.HasOne(ce => ce.Student)
               .WithMany(s => s.CourseEnrollments)
               .HasForeignKey(ce => ce.StudentId);

        builder.HasOne(ce => ce.Course)
               .WithMany(c => c.CourseEnrollments)
               .HasForeignKey(ce => ce.CourseId);

        // BaseEntity alanları
        builder.Property(ce => ce.CreatedAt).IsRequired();
        builder.Property(ce => ce.UpdatedAt);
        builder.Property(ce => ce.CreatedBy);
        builder.Property(ce => ce.UpdatedBy);
    }
}
