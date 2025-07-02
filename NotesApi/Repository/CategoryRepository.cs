using Microsoft.EntityFrameworkCore;
using NotesApi.Data;
using NotesApi.Interfaces.Repository;
using NotesApi.Interfaces.Sevices;
using NotesApi.Models;

namespace NotesApi.Repository;

public class CategoryRepository(AppDbContext context, ICurrentUserService current) : ICategoryRepository
{
    private readonly AppDbContext _context = context;
    private readonly ICurrentUserService _current = current;

    public async Task<IEnumerable<Category>> GetAllAsync() =>
         await _context.Categories
            .OrderBy(c => c.Name)
            .Where(c => c.UserId == _current.UserId)
            .ToListAsync();

    public async Task<Category?> GetByIdAsync(int id) =>
        await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id && c.UserId == _current.UserId);

    public async Task<Category?> GetByNameAsync(string name) =>
        await _context.Categories
            .FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower() && c.UserId == _current.UserId);

    public async Task<Category> CreateAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<Category> UpdateAsync(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id && x.UserId == _current.UserId);
        if (category is null) return false;
    
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> ExistsAsync(int id) =>
         await _context.Categories.AnyAsync(c => c.Id == id && c.UserId == _current.UserId);

    public async Task<bool> HasNotesAsync(int id) =>
        await _context.Notes.AnyAsync(n => n.CategoryId == id && n.UserId == _current.UserId);
}