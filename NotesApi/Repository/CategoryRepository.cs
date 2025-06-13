using Microsoft.EntityFrameworkCore;
using NotesApi.Data;
using NotesApi.Interfaces.Repository;
using NotesApi.Models;

namespace NotesApi.Repository;

public class CategoryRepository(AppDbContext context) : ICategoryRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<Category>> GetAllAsync() =>
         await _context.Categories
            .OrderBy(c => c.Name)
            .ToListAsync();

    public async Task<Category?> GetByIdAsync(int id) =>
        await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id);

    public async Task<Category?> GetByNameAsync(string name) =>
        await _context.Categories
            .FirstOrDefaultAsync(c => c.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));

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

    public async Task<bool> DeleteAsync(int id) =>
        (await _context.Categories.FindAsync(id)) is Category category 
        && (_context.Categories.Remove(category), await _context.SaveChangesAsync()).Item2 > 0;

    public async Task<bool> ExistsAsync(int id) =>
         await _context.Categories.AnyAsync(c => c.Id == id);

    public async Task<bool> HasNotesAsync(int id) =>
        await _context.Notes.AnyAsync(n => n.CategoryId == id);
}