using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Model.Request.Authentications;
using Darwin.Service.Common;
using Darwin.Service.TokenOperations;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Darwin.Service.Features.Auth.RefreshToken;

public static class CreateTokenByRefreshToken
{
    public record Command(RefreshTokenRequest Model) :ICommand<DarwinResponse<TokenResponse>>;

    public class CommandHandler : ICommandHandler<Command, DarwinResponse<TokenResponse>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public CommandHandler(UserManager<AppUser> userManager,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<DarwinResponse<TokenResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            ClaimsPrincipal? principal=_tokenService.GetPrincipalFromExpiredToken(request.Model.AccessToken); //Gelen Access token belirlediğimiz şartlara uyuyor mu kontrolleri yapılıyor ve süresi dolmuş mu kontrolü yapılıyor.
            
            var emailFromToken = principal.FindFirstValue(ClaimTypes.Email);

            AppUser? user = await _userManager.FindByEmailAsync(emailFromToken);
            var roles= await _userManager.GetRolesAsync(user);

            if (user.RefreshTokenExpiration <= DateTime.UtcNow)
                throw new Exception("Lütfen yeniden giriş yapınız.");

            var newToken= await _tokenService.CreateTokenAsync(user);

            user.RefreshToken = newToken.RefreshToken;
            await _userManager.UpdateAsync(user);

            return DarwinResponse<TokenResponse>.Success(newToken, 200);
        }
    }

}
