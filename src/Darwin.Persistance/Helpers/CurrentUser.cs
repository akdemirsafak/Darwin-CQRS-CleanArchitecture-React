using Darwin.Application.Helper;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Darwin.Persistance.Helper;

public class CurrentUser : ICurrentUser
{
    private IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public string GetUserId => _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

}
