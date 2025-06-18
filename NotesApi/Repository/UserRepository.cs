using Microsoft.EntityFrameworkCore;
using NotesApi.Data;
using NotesApi.Interfaces.Repository;
using NotesApi.Models;

namespace NotesApi.Repository;
public class UserRepository(AppDbContext context) : IUserRepository
{
    private readonly AppDbContext _context = context;
    public async Task<User> CreateAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> ExistsAsync(string username)
    {
        if (username is string usernameString)
        {
            return await _context.Users.AnyAsync(u => u.Username == usernameString);
        }
        throw new ArgumentException("Invalid username type", nameof(username));
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        if (username is string usernameString)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == usernameString);
        }
        throw new ArgumentException("Invalid username type", nameof(username));
    }
}
