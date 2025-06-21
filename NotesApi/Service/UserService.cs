using NotesApi.DTOs;
using NotesApi.Exceptions;
using NotesApi.Interfaces.Repository;
using NotesApi.Interfaces.Sevices;
using NotesApi.JwtAuth;
using NotesApi.Models;

namespace NotesApi.Service;
public class UserService(IUserRepository userRepository, JwtService jwtService) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<UserDto> CreateUserAsync(UserRegisterDto userDto)
    {
        if (await _userRepository.ExistsAsync(userDto.Username))
        {
            throw new ApiException("User already exists.");
        }

        await _userRepository.CreateAsync(new User
        {
            Username = userDto.Username,
            Email = userDto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password)
        });
        return new UserDto(userDto.Username, userDto.Email);
    }

    public async Task<string> LoginAsync(UserLoginDto userLoginDto)
    {
        User user = await _userRepository.GetByEmailAsync(userLoginDto.Email);
        if (!BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.PasswordHash))
            throw new ApiException("Invalid password.");

        return jwtService.GenerateToken(user);
    }
}
