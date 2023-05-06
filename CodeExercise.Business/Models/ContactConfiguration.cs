using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeExercise.Models;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id)
            .HasColumnName("id")
            .IsRequired();
        builder.Property(t => t.FirstName)
            .HasColumnName("first_name")
            .HasMaxLength(50);
        builder.Property(t => t.LastName)
            .HasColumnName("last_name")
            .HasMaxLength(50);
        builder.Property(t => t.Email)
            .HasColumnName("email")
            .HasMaxLength(50);
           
        builder.ToTable("contacts");
    }
}