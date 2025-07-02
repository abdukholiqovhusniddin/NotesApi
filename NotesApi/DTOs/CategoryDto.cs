using System.ComponentModel.DataAnnotations;

namespace NotesAPI.DTOs;
public record CategoryDto(int Id, string Name, int UserId);

public class CategoryInputDto
{
    [Required(ErrorMessage = "Category name is required")]
    [StringLength(50, ErrorMessage = "Category name cannot exceed 50 characters")]
    public string Name { get; set; } = string.Empty;
    //public int UserId { get; set; }
}
