using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NotesApi.DTOs;

namespace NotesApi.JwtAuth;
public class JwtService(IOptions<AuthSettings> options)
{ 
    public string GenerateToken(UserDto account)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, account.Id.ToString()),
            new(ClaimTypes.Email, account.Email),
            new(ClaimTypes.Name, account.Username)
        };
        var jwtToken = new JwtSecurityToken(
           //expires: DateTime.UtcNow.Add(options.Value.Expres),
           claims: claims,
           signingCredentials: new SigningCredentials(
               new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SecretKey)),
               SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}
