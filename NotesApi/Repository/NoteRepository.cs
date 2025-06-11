using Microsoft.EntityFrameworkCore;
using NotesApi.Data;
using NotesApi.Models;

namespace NotesApi.Repository;
public class NoteRepository(AppDbContext context):INoteRepository
{
    private readonly AppDbContext _context = context;
    public async Task<List<Note>> GetAllAsync()
    {
        return await _context.Notes.Include(n => n.Category).ToListAsync();
    }
    public async Task<Note?> GetByIdAsync(int id)
    {
        return await _context.Notes.Include(n => n.Category).FirstOrDefaultAsync(n => n.Id == id);
    }
    public async Task<Note> CreateAsync(Note note)
    {
        _context.Notes.Add(note);
        await _context.SaveChangesAsync();
        return note;
    }
    public async Task<Note?> UpdateAsync(int id, Note note)
    {
        var existing = await _context.Notes.FindAsync(id);
        if (existing == null) return null;

        existing.Title = note.Title;
        existing.Content = note.Content;
        existing.CategoryId = note.CategoryId;
        existing.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return existing;
    }
    public async Task<Note?> DeleteAsync(int id)
    {
        var note = await _context.Notes.FindAsync(id);
        if (note == null) return null;
        _context.Notes.Remove(note);
        await _context.SaveChangesAsync();
        return note;
    }
}