using Darwin.Core.Entities;

namespace Darwin.Service.TokenOperations;

public interface ITokenService
{
    TokenResponse CreateToken(AppUser appUser);
}
