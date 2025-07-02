using NotesApi.DTOs;
using NotesApi.Models;

namespace NotesApi.Interfaces.Repository;

public interface INoteRepository
{
    Task<(IEnumerable<Note> Notes, int TotalCount)> GetAllAsync(
            int UserId,
            int? categoryId = null,
            string? search = null,
            int page = 1,
            int pageSize = 10);
    Task<Note?> GetByIdAsync(int id, int userId);
    Task<Note> CreateAsync(Note note);
    Task<Note> UpdateAsync(Note note);
    Task<bool> DeleteAsync(int id, int userId);
    Task<bool> ExistsAsync(int id, int userId);
}