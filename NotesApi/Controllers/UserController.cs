using Microsoft.AspNetCore.Mvc;
using NotesApi.DTOs;
using NotesApi.Interfaces.Sevices;

namespace NotesApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService service): ControllerBase
{
    private readonly IUserService _userService = service;

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> CreateUser(UserRegisterDto userRegisterDto)
    {
        UserDto createdUser = await _userService.CreateUserAsync(userRegisterDto);
        if (createdUser == null)
        {
            return BadRequest(new { message = "User with this email already exists" });
        }
        return CreatedAtAction(nameof(CreateUser), new { email = userRegisterDto.Email }, createdUser);
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(UserLoginDto userLoginDto)
    {
        var token = await _userService.LoginAsync(userLoginDto);
        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized(new { message = "Invalid email or password" });
        }
        return Ok(token);
    }
}
