namespace NotesApi.Models;
public class Note: BaseEntity
{
    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public int CategoryId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public Category Category { get; set; } = null!;

    public int UserId { get; set; }
    public User? User { get; set; }
}
