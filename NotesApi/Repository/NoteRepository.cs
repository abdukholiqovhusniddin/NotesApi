using Microsoft.EntityFrameworkCore;
using NotesApi.Data;
using NotesApi.Interfaces.Repository;
using NotesApi.Models;

namespace NotesApi.Repository;
public class NoteRepository(AppDbContext context):INoteRepository
{
    private readonly AppDbContext _context = context;
    public async Task<(IEnumerable<Note> Notes, int TotalCount)> GetAllAsync(
            int userId,
            int? categoryId = null,
            string? search = null,
            int page = 1,
            int pageSize = 10
            )
    {
        var query = _context.Notes
            .Include(n => n.Category)
            .Where(n => n.UserId == userId)
            .AsQueryable();


        if (categoryId.HasValue)
            query = query.Where(n => n.CategoryId == categoryId.Value);

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
            .Where(n => n.UserId == userId)
            .ToListAsync();

        return (notes, totalCount);
    }

    public async Task<Note?> GetByIdAsync(int id, int userId) =>
        await _context.Notes
            .Include(n => n.Category)
            .FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);

    public async Task<Note> CreateAsync(Note note)
    {
        await _context.Notes.AddAsync(note);
        await _context.SaveChangesAsync();

        await _context.Entry(note)
            .Reference(n => n.Category)
            .LoadAsync();

        return note;
    }

    public async Task<Note> UpdateAsync(Note note)
    {
        _context.Notes.Update(note);
        await _context.SaveChangesAsync();

        await _context.Entry(note)
            .Reference(n => n.Category)
            .LoadAsync();

        return note;
    }

    public async Task<bool> DeleteAsync(int id, int userId)
    {
        var note =  _context.Notes.FirstOrDefault(n => n.Id == id && n.UserId == userId);
        if (note is null) return false;

        _context.Notes.Remove(note);
        await _context.SaveChangesAsync();
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> ExistsAsync(int id, int userId) =>
        await _context.Notes.AnyAsync(n => n.Id == id && n.UserId == userId);
}