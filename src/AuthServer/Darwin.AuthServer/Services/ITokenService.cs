using Darwin.AuthServer.Entities;
using Darwin.Shared.Dtos;
using System.Security.Claims;

namespace Darwin.AuthServer.Services;

public interface ITokenService
{
    Task<TokenResponse> CreateTokenAsync(AppUser appUser);
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    Task<TokenResponse> CreateTokenByRefreshToken(string refreshToken, string accessToken);
}