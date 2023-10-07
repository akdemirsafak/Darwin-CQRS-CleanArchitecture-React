using Darwin.Core.Entities;

namespace Darwin.Service.TokenOperations;

public interface ITokenService
{
    Task<TokenResponse> CreateTokenAsync(AppUser appUser);
}
