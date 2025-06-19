using Microsoft.EntityFrameworkCore;
using NotesApi.Data;
using NotesApi.Exceptions;
using NotesApi.Interfaces.Repository;
using NotesApi.Models;

namespace NotesApi.Repository;
public class UserRepository(AppDbContext context): IUserRepository
{
    private readonly AppDbContext _context = context;
    public async Task CreateAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(string username)
    {
        if(string.IsNullOrWhiteSpace(username))
            throw new ApiException("Username cannot be null or empty.");
        return await _context.Users.AnyAsync(n => n.Username == username);
    }

    public async Task<User> GetByEmailAsync(string email) =>
        await _context.Users.FirstOrDefaultAsync(u => u.Email == email) 
            ?? throw new ApiException("User with this email does not exist.");
}
