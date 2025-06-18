using NotesApi.DTOs;
using NotesApi.Exceptions;
using NotesApi.Interfaces.Repository;
using NotesApi.Interfaces.Sevices;
using NotesApi.JwtAuth;
using NotesApi.Models;
namespace NotesApi.Service;
public class UserService(IUserRepository userRepository, JwtService jwtService): IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<UserRegisterDto> CreateUserAsync(UserRegisterDto userRegisterDto)
    {
        if (await _userRepository.ExistsAsync(userRegisterDto.Username))
            throw new ApiException("User already exists.");

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

    public async Task<string?> LoginAsync(UserDto dto)
    {
        var user = await _userRepository.GetByUsernameAsync(dto.Username);
        if (user is null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
        {
            throw new ApiException("Invalid username or password.");
        }

        var userDto = new UserDto
        {
            Username = user.Username,
            Email = user.Email,
            Id = user.Id,
            Password = dto.Password
        };

        return jwtService.GenerateToken(userDto);
    }
}
