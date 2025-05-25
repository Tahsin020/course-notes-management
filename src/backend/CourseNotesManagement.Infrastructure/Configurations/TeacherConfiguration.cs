using CourseNotesManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseNotesManagement.Infrastructure.Configurations;

public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.HasBaseType<User>();
        builder.HasMany(t => t.CourseAssignments)
               .WithOne(ca => ca.Teacher)
               .HasForeignKey(ca => ca.TeacherId);
    }
}