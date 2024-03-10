using Darwin.Domain.Dtos;
using Darwin.Domain.Entities;
using Darwin.Domain.RequestModels.Users;

namespace Darwin.Application.Services;

public interface IAuthService
{
    Task<AppUser> RegisterAsync(RegisterRequest request);
    Task<TokenResponse> LoginAsync(LoginRequest request);
    Task RevokeTokensAsync();
    Task RevokeTokenByEmailAsync(string email);
}
