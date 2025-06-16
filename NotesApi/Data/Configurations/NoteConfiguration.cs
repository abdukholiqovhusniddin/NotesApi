using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotesApi.Models;

namespace NotesApi.Data.Configurations;
public class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.HasKey(n => n.Id);

        builder.Property(n => n.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(n => n.Content)
            .IsRequired().
            HasMaxLength(10000);

        builder.Property(n => n.CreatedAt)
            .IsRequired();

        builder.HasOne(n => n.Category)
               .WithMany(c => c.Notes)
               .HasForeignKey(n => n.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);


        builder.Property(n => n.UserId)
            .IsRequired();

        builder.HasOne(n => n.User)
               .WithMany(u => u.Notes)
               .HasForeignKey(n => n.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
