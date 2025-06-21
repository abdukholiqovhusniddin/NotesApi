using System.Security.Claims;
using NotesApi.Interfaces.Sevices;

namespace NotesApi.Service;
public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    public int UserId => IsAuthenticated
        ? int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)
        : 0;
    public bool IsAuthenticated => _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
}
