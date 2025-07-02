using System.ComponentModel.DataAnnotations;

namespace NotesApi.Models;
public class Category: BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public virtual ICollection<Note> Notes { get; set; } = [];
    public int UserId { get; set; }
}
