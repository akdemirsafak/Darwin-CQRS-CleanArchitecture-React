using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Model.Request.Authentications;
using Darwin.Service.Common;
using Darwin.Service.TokenOperations;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace Darwin.Service.Features.Authentications;

public static class Register
{
    public record Command(RegisterRequest Model) : ICommand<DarwinResponse<TokenResponse>>;


    public class CommandHandler : ICommandHandler<Command, DarwinResponse<TokenResponse>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public CommandHandler(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<DarwinResponse<TokenResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var appUser = request.Model.Adapt<AppUser>();
            appUser.BirthDate = DateTime.UtcNow.AddYears(-15);
            var registerResult = await _userManager.CreateAsync(appUser, request.Model.Password);
            if (!registerResult.Succeeded)
            {
                return DarwinResponse<TokenResponse>.Fail(registerResult.Errors.Select(x => x.Description).ToList());
            }
            return DarwinResponse<TokenResponse>.Success(await _tokenService.CreateTokenAsync(appUser), 201);
        }
    }
}
