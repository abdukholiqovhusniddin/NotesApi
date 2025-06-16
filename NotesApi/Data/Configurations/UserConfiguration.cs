using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotesApi.Models;

namespace NotesApi.Data.Configurations;
public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(n => n.Id);

        builder.Property(n => n.Username)
            .IsRequired().
            HasMaxLength(50);

        builder.HasIndex(n => n.Username)
            .IsUnique();

        builder.Property(n => n.Email)
            .IsRequired()
            .HasMaxLength(50);
        builder.HasIndex(n => n.Email)
            .IsUnique();

        builder.Property(n => n.PasswordHash)
            .IsRequired()
            .HasMaxLength(256);

        builder.HasMany(n => n.Notes)
            .WithOne(n => n.User)
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
