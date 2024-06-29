using Darwin.AuthServer.Models.Requests.Auth;
using Darwin.AuthServer.Models.Responses.Users;
using Darwin.Shared.Dtos;
using Microsoft.AspNetCore.Identity.Data;

namespace Darwin.AuthServer.Services;

public interface IAuthService
{
    Task<DarwinResponse<GetUserResponse>> RegisterAsync(Models.Requests.Auth.RegisterRequest request);
    Task<DarwinResponse<TokenResponse>> LoginAsync(LoginRequest request);
    Task<DarwinResponse<NoContentDto>> ForgotPasswordAsync(string email);
    Task<DarwinResponse<NoContentDto>> ResetPasswordAsync(string newPassword, string userId, string token); //şifremi unuttumdan gelen
    Task<DarwinResponse<TokenResponse>> CreateAccessTokenByRefreshTokenAsync(CreateTokenByRefreshTokenRequest request);
    Task<DarwinResponse<NoContentDto>> RevokeTokensAsync();
    Task<DarwinResponse<NoContentDto>> RevokeTokenByEmailAsync(string email);
}
