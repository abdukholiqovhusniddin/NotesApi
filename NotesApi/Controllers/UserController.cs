using Microsoft.AspNetCore.Mvc;
using NotesApi.DTOs;
using NotesApi.Interfaces.Sevices;

namespace NotesApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService ?? throw new ArgumentNullException(nameof(userService));

    [HttpPost]
    [Route("register")]
    public async Task<ActionResult<UserRegisterDto>> CreateUser(UserRegisterDto userRegisterDto)
    {
        await _userService.CreateUserAsync(userRegisterDto);
        return CreatedAtAction(nameof(CreateUser), new { id = userRegisterDto.Username }, userRegisterDto);
        
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(UserDto dto)
    {
        int Id = await _userService.LoginAsync(dto);
        if (Id == 0)
            return Unauthorized("Invalid email or password.");
        return Ok(Id);
    }
}
