using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeExercise.Models;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id)
            .HasColumnName("id")
            .IsRequired();
        builder.Property(t => t.Name)
            .HasColumnName("dept_name")
            .HasMaxLength(50);
        builder.Property(t => t.Description)
            .HasColumnName("dept_description")
            .HasMaxLength(50);
        builder.ToTable("departments");
    }
}