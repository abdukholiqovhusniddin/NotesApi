using NotesApi.DTOs;

namespace NotesApi.Interfaces.Sevices;
public interface IUserService
{
    Task<UserDto> CreateUserAsync(UserRegisterDto userDto);
    Task<string> LoginAsync(UserLoginDto userLoginDto);
}
