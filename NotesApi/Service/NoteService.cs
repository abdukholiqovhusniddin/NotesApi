using NotesApi.Models;
using NotesApi.Repository;

namespace NotesApi.Service;
public class NoteService(INoteRepository noteRepository) : INoteService
{
    private readonly INoteRepository _noteRepository = noteRepository;

    public async Task<List<Note>> GetAllAsync() => await _noteRepository.GetAllAsync();
    public async Task<Note> GetByIdAsync(int id) => await _noteRepository.GetByIdAsync(id);
    public async Task<Note> CreateAsync(Note note) => await _noteRepository.CreateAsync(note);
    public async Task<Note> UpdateAsync(int id, Note note) => await _noteRepository.UpdateAsync(id, note);
    public async Task<Note?> DeleteAsync(int id) => await _noteRepository.DeleteAsync(id);
}
