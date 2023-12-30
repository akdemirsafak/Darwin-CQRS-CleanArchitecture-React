using Darwin.Core.Entities;
using Darwin.Service.TokenOperations;

namespace Darwin.Service.Services;

public interface IAuthenticationService
{
    Task<TokenResponse> CreateTokenAsync(AppUser user);
    string GenerateRefreshToken();
    Task RevokeRefreshToken(string refreshToken);
    Task<TokenResponse> CreateTokenByRefreshTokenAsync(string refreshToken);

}
