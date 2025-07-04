using Microsoft.AspNetCore.Mvc;
using NotesApi.DTOs;
using NotesApi.Interfaces.Sevices;
using NotesApi.Models;

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
            return BadRequest((new ApiResponse<object>
            {
                Error = "User with this email already exists.",
                StatusCode = 400
            }));
        }
        return Ok((new ApiResponse<object>
        {
            Data = createdUser,
            StatusCode = 200
        }));
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
        return Ok((new ApiResponse<object>
        {
            Data = token,
            StatusCode = 200
        }));
    }
}
