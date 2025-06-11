using NotesApi.Models;

namespace NotesApi.Service;
public interface INoteService
{
    Task<List<Note>> GetAllAsync();
    Task<Note> GetByIdAsync(int id);
    Task<Note> CreateAsync(Note note);
    Task<Note> UpdateAsync(int id, Note note);
    Task<Note?> DeleteAsync(int id);
}
