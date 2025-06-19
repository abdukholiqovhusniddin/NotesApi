
using NotesApi.Models;

namespace NotesApi.Interfaces.Repository;
public interface IUserRepository
{
    Task CreateAsync(User user);
    Task<bool> ExistsAsync(string email);
    Task<User> GetByEmailAsync(string email);
}
