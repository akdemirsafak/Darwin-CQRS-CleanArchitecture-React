using Darwin.Core.Entities;
using System.Security.Claims;

namespace Darwin.Service.TokenOperations;

public interface ITokenService
{
    Task<TokenResponse> CreateTokenAsync(AppUser appUser);
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}
