using System.ComponentModel.DataAnnotations;

namespace NotesApi.Models;
public class Category: BaseEntity
{
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
    public virtual ICollection<Note> Notes { get; set; } = [];
}
