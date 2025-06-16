using System.ComponentModel.DataAnnotations;

namespace NotesAPI.DTOs;
public class NoteDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class NoteInputDto
{
    [Required(ErrorMessage = "User ID is required")]
    public int UserId { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 100 characters")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Content is required")]
    [StringLength(10000, MinimumLength = 5, ErrorMessage = "Content must be at least 5 characters")]
    public string Content { get; set; } = string.Empty;

    [Required(ErrorMessage = "Category is required")]
    public int CategoryId { get; set; }
}