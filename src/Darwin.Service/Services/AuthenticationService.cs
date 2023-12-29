using Darwin.Core.Entities;
using Darwin.Service.TokenOperations;

namespace Darwin.Service.Services;

public class AuthenticationService : IAuthenticationService
{
    public Task<TokenResponse> CreateTokenAsync(AppUser user)
    {
        throw new NotImplementedException();
    }

    public Task<TokenResponse> CreateTokenByRefreshTokenAsync(string refreshToken)
    {
        throw new NotImplementedException();
    }

    public string GenerateRefreshToken()
    {
        throw new NotImplementedException();
    }

    public Task RevokeRefreshToken(string refreshToken)
    {
        throw new NotImplementedException();
    }
}
