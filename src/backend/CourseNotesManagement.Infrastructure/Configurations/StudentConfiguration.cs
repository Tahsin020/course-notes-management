using CourseNotesManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseNotesManagement.Infrastructure.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasBaseType<User>();

        builder.HasOne(s => s.Parent)
               .WithMany(p => p.Children)
               .HasForeignKey(s => s.ParentId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(s => s.CourseEnrollments)
               .WithOne(ce => ce.Student)
               .HasForeignKey(ce => ce.StudentId);
    }
}