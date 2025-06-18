using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApi.DTOs;
using NotesApi.Interfaces.Sevices;

namespace NotesApi.Controllers;
[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService ?? throw new ArgumentNullException(nameof(userService));

    [HttpPost]
    [Route("register")]
    [AllowAnonymous]
    public async Task<ActionResult<UserRegisterDto>> CreateUser(UserRegisterDto userRegisterDto)
    {
        await _userService.CreateUserAsync(userRegisterDto);
        return CreatedAtAction(nameof(CreateUser), new { id = userRegisterDto.Username }, userRegisterDto);
        
    }

    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(UserDto dto)
    {
        var token = await _userService.LoginAsync(dto);
        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized(new { message = "Invalid username or password" });
        }
        return Ok(token);
    }
}
