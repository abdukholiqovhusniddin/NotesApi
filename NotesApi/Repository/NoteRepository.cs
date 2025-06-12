using Microsoft.EntityFrameworkCore;
using NotesApi.Data;
using NotesApi.Models;

namespace NotesApi.Repository;
public class NoteRepository(AppDbContext context):INoteRepository
{
    private readonly AppDbContext _context = context;
    public async Task<(IEnumerable<Note> Notes, int TotalCount)> GetAllAsync(
            int? categoryId = null,
            string? search = null,
            int page = 1,
            int pageSize = 10)
    {
        var query = _context.Notes.Include(n => n.Category).AsQueryable();

        if (categoryId.HasValue)
        {
            query = query.Where(n => n.CategoryId == categoryId.Value);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            var searchTerm = search.ToLower();
            query = query.Where(n =>
                n.Title.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase) ||
                n.Content.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase));
        }

        var totalCount = await query.CountAsync();

        var notes = await query
            .OrderByDescending(n => n.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (notes, totalCount);
    }

    public async Task<Note?> GetByIdAsync(int id)
    {
        return await _context.Notes
            .Include(n => n.Category)
            .FirstOrDefaultAsync(n => n.Id == id);
    }

    public async Task<Note> CreateAsync(Note note)
    {
        note.CreatedAt = DateTime.UtcNow;
        _context.Notes.Add(note);
        await _context.SaveChangesAsync();

        await _context.Entry(note)
            .Reference(n => n.Category)
            .LoadAsync();

        return note;
    }

    public async Task<Note> UpdateAsync(Note note)
    {
        note.UpdatedAt = DateTime.UtcNow;
        _context.Notes.Update(note);
        await _context.SaveChangesAsync();

        await _context.Entry(note)
            .Reference(n => n.Category)
            .LoadAsync();

        return note;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var note = await _context.Notes.FindAsync(id);
        if (note == null) return false;

        _context.Notes.Remove(note);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Notes.AnyAsync(n => n.Id == id);
    }
}