using Darwin.Web.Models;

namespace Darwin.Web.Services;

public interface IAuthService
{
    Task<bool> Register(RegisterDto registerDto);
    Task<bool> Login(LoginDto loginDto);
    Task<bool> Logout();
}
