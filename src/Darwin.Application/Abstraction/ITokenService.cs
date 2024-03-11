using Darwin.Domain.Dtos;
using Darwin.Domain.Entities;
using System.Security.Claims;

namespace Darwin.Application.TokenOperations;

public interface ITokenService
{
    Task<TokenResponse> CreateTokenAsync(AppUser appUser);
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    Task<TokenResponse> CreateTokenByRefreshToken(string refreshToken, string accessToken);
}