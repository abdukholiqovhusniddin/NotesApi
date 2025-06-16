using NotesApi.DTOs;
using NotesApi.Models;

namespace NotesApi.Interfaces.Repository;
public interface IUserRepository
{
    Task<User> CreateAsync(User user);
    Task<bool> ExistsAsync(string username);
    Task<User?> GetByUsernameAsync(string username);
}
