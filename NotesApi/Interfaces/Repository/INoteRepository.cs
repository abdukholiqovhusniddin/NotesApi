using NotesApi.Models;

namespace NotesApi.Interfaces.Repository;

public interface INoteRepository
{
    Task<(IEnumerable<Note> Notes, int TotalCount)> GetAllAsync(
            int? categoryId = null,
            string? search = null,
            int page = 1,
            int pageSize = 10);
    Task<Note?> GetByIdAsync(int id);
    Task<Note> CreateAsync(Note note);
    Task<Note> UpdateAsync(Note note);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}