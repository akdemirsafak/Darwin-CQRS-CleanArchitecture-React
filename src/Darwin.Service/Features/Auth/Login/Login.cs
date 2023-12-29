using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Model.Request.Users;
using Darwin.Service.Common;
using Darwin.Service.TokenOperations;
using Microsoft.AspNetCore.Identity;

namespace Darwin.Service.Features.Auth.Login;

public static class Login
{
    public record Command(LoginRequest Model) : ICommand<DarwinResponse<TokenResponse>>;


    public class CommandHandler : ICommandHandler<Command, DarwinResponse<TokenResponse>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CommandHandler(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService,
            IGenericRepository<UserRefreshToken> userRefreshTokenRepository,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _userRefreshTokenRepository = userRefreshTokenRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DarwinResponse<TokenResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var existUser = await _userManager.FindByEmailAsync(request.Model.Email);


            var loginResult = await _signInManager.CheckPasswordSignInAsync(existUser, request.Model.Password, true);

            // User Locked
            if (loginResult.IsLockedOut)
                return DarwinResponse<TokenResponse>.Fail("User locked.Try again 5 min.", 400);

            //Another Login Problems
            if (!loginResult.Succeeded)
                return DarwinResponse<TokenResponse>.Fail("Login Failed.", 400);


            var userRefreshToken = await _userRefreshTokenRepository.GetAsync(x => x.UserId == existUser.Id);

            var token = await _tokenService.CreateTokenAsync(existUser);

            //User'ın refresh token'ı yoksa
            if (userRefreshToken is null)
            {
                var newRefreshToken = new UserRefreshToken()
                {
                    UserId = existUser.Id,
                    Code = token.RefreshToken,
                    Expiration = token.RefreshTokenExpiration
                };
                await _userRefreshTokenRepository.CreateAsync(newRefreshToken);
            }
            else
            {
                userRefreshToken.Code = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
            }
            await _unitOfWork.CommitAsync();


            //Last Login işlemi yaptık.
            existUser.LastLogin = DateTime.UtcNow;
            
            var updateResult = await _userManager.UpdateAsync(existUser);

            await _userManager.UpdateSecurityStampAsync(existUser); // TODO !
            await _userManager.SetAuthenticationTokenAsync(existUser, "Default", "AccessToken", token.AccessToken);

            if (!updateResult.Succeeded)
                return DarwinResponse<TokenResponse>.Fail("Login cannot updated.", 500);

            return DarwinResponse<TokenResponse>.Success(token, 200);
        }
    }
}

