using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotesApi.Models;

namespace NotesApi.Data.Configurations;
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(c => c.Name)
            .IsUnique();

        builder.HasData(new Category { Id = 1, Name = "Work" },
                        new Category { Id = 2, Name = "Personal" },
                        new Category { Id = 3, Name = "Study" });
    }
}
