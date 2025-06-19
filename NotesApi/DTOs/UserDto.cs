using System.ComponentModel.DataAnnotations;
using NotesApi.Models;

namespace NotesApi.DTOs;
public record UserDto(string Username, string Email);

public class UserRegisterDto
{
    [Required(ErrorMessage = "Username is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address format")]
    public string Email { get; set; } = string.Empty;

    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;
}
public class UserLoginDto
{
    [Required(ErrorMessage = "Email is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Email must be between 3 and 50 characters")]
    [EmailAddress(ErrorMessage = "Invalid email address format")]
    public string Email { get; set; } = string.Empty;


    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    public string Password { get; set; } = string.Empty;
}