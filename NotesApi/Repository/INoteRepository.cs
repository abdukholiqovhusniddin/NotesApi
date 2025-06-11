using NotesApi.Models;

namespace NotesApi.Repository;

public interface INoteRepository
{
    Task<List<Note>> GetAllAsync();
    Task<Note?> GetByIdAsync(int id);
    Task<Note?> CreateAsync(Note note);
    Task<Note?> UpdateAsync(int id, Note note);
    Task<Note?> DeleteAsync(int id);

}