using System.ComponentModel.DataAnnotations;

namespace NotesApi.Models;
public class User: BaseEntity
{
    public string Username { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;
    public List<Note> Notes { get; set; } = [];
}
