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
        private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CommandHandler(UserManager<AppUser> userManager,
            ITokenService tokenService,
            IGenericRepository<UserRefreshToken> userRefreshTokenRepository,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _userRefreshTokenRepository = userRefreshTokenRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DarwinResponse<TokenResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            ClaimsPrincipal? principal=_tokenService.GetPrincipalFromExpiredToken(request.Model.AccessToken); //Gelen Access token belirlediğimiz şartlara uyuyor mu kontrolleri yapılıyor ve süresi dolmuş mu kontrolü yapılıyor.
            
            var emailFromToken = principal.FindFirstValue(ClaimTypes.Email);

            AppUser? user = await _userManager.FindByEmailAsync(emailFromToken);
            var roles= await _userManager.GetRolesAsync(user);

            UserRefreshToken? userRefreshToken= await _userRefreshTokenRepository.GetAsync(x=>x.UserId==user.Id);

            if (userRefreshToken.Expiration <= DateTime.UtcNow || userRefreshToken.UserId!=user.Id)
                throw new Exception("Lütfen yeniden giriş yapınız.");

            var newToken= await _tokenService.CreateTokenAsync(user);

            userRefreshToken.Code = newToken.RefreshToken;
            await _unitOfWork.CommitAsync();
            return DarwinResponse<TokenResponse>.Success(newToken, 200);
        }
    }

}
