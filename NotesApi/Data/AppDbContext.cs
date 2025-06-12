using Microsoft.EntityFrameworkCore;
using NotesApi.Models;

namespace NotesApi.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 
        Database.EnsureCreated();
    }
    public DbSet<Note> Notes { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Name).IsUnique();
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
        });

        modelBuilder.Entity<Note>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Content).IsRequired().HasMaxLength(10000);
            entity.Property(e => e.CreatedAt).IsRequired();

            entity.HasOne(n => n.Category)
                  .WithMany(c => c.Notes)
                  .HasForeignKey(n => n.CategoryId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Work" },
            new Category { Id = 2, Name = "Personal" },
            new Category { Id = 3, Name = "Study" }
        );
    }

}
