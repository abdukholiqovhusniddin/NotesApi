using NotesApi.DTOs;
using NotesApi.Models;

namespace NotesApi.Interfaces.Sevices;
public interface IUserService
{
    Task<UserRegisterDto> CreateUserAsync(UserRegisterDto userRegisterDto);
    Task<string?> LoginAsync(UserDto dto);
}
