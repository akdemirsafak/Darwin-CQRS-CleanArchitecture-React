using Darwin.AuthServer.Models.Requests.Auth;
using Darwin.AuthServer.Services;
using Darwin.Shared.Base;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.AuthServer.Controllers;
[Route("api/[controller]/[action]")]
public class AuthController : CustomBaseController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        return CreateActionResult(await _authService.LoginAsync(request));
    }
    [HttpPost]
    public async Task<IActionResult> Register(Models.Requests.Auth.RegisterRequest request)
    {
        return CreateActionResult(await _authService.RegisterAsync(request));
    }
    [HttpPost]
    public async Task<IActionResult> ForgotPassword(string email) //User'a mail gönderilir.
    {
        return CreateActionResult(await _authService.ForgotPasswordAsync(email));
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(string newPassword, string userId, string token)
    {
        return CreateActionResult(await _authService.ResetPasswordAsync(newPassword, userId, token));
    }

    [HttpPost] // Refresh token'dan AccessToken ve RefreshToken oluşturur.
    public async Task<IActionResult> RefreshToken(CreateTokenByRefreshTokenRequest request) //CreateTokenByRefreshToken
    {
        return CreateActionResult(await _authService.CreateAccessTokenByRefreshTokenAsync(request));
    }
    [HttpPost]
    public async Task<IActionResult> RevokeToken(string email)
    {
        return CreateActionResult(await _authService.RevokeTokenByEmailAsync(email));
    }
    [HttpPost]
    public async Task<IActionResult> RevokeAllTokens()
    {
        return CreateActionResult(await _authService.RevokeTokensAsync());
    }
}
