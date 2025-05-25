using CourseNotesManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseNotesManagement.Infrastructure.Configurations;

public class ParentConfiguration : IEntityTypeConfiguration<Parent>
{
    public void Configure(EntityTypeBuilder<Parent> builder)
    {
        builder.HasBaseType<User>();

        builder.HasMany(p => p.Children)
               .WithOne(s => s.Parent)
               .HasForeignKey(s => s.ParentId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
