using NotesApi.DTOs;
using NotesApi.Interfaces.Repository;
using NotesApi.Interfaces.Sevices;
using NotesApi.Models;
namespace NotesApi.Service;
public class UserService(IUserRepository userRepository): IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<UserRegisterDto> CreateUserAsync(UserRegisterDto userRegisterDto)
    {
        if (await _userRepository.ExistsAsync(userRegisterDto.Username))
            throw new InvalidOperationException("User already exists.");
        await _userRepository.CreateAsync(new User
        {
            Username = userRegisterDto.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(userRegisterDto.Password),
            Email = userRegisterDto.Email
        });
        return new UserRegisterDto { 
            Username = userRegisterDto.Username, 
            Email = userRegisterDto.Email 
        };
    }

    public async Task<int> LoginAsync(UserDto dto)
    {
        var user = await _userRepository.GetByUsernameAsync(dto.Username);
        if (user is null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return 0;
        int Id = user.Id;
        return Id;
    }


}
