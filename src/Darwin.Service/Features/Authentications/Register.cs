using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Model.Request.Authentications;
using Darwin.Service.Common;
using Darwin.Service.Notifications.UserCreated;
using Darwin.Service.TokenOperations;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Darwin.Service.Features.Authentications;

public static class Register
{
    public record Command(RegisterRequest Model) : ICommand<DarwinResponse<TokenResponse>>;


    public class CommandHandler : ICommandHandler<Command, DarwinResponse<TokenResponse>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IPublisher _publisher;

        public CommandHandler(UserManager<AppUser> userManager, ITokenService tokenService, IPublisher publisher)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _publisher = publisher;
        }

        public async Task<DarwinResponse<TokenResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var appUser = request.Model.Adapt<AppUser>();
            appUser.BirthDate = DateTime.UtcNow.AddYears(-15);
            appUser.CreatedAt = DateTime.UtcNow;
            var registerResult = await _userManager.CreateAsync(appUser, request.Model.Password);
            if (!registerResult.Succeeded)
            {
                return DarwinResponse<TokenResponse>.Fail(registerResult.Errors.Select(x => x.Description).ToList());
            }
            var userCreatedEventModel=new UserCreatedMailModel(appUser.Email!,"burasıConfirmLinki",appUser.UserName!,appUser.CreatedAt);

            await _publisher.Publish(new UserCreatedCreateFavoritePlaylistEvent(appUser.Id), cancellationToken);
            await _publisher.Publish(new UserCreatedSendMailEvent(userCreatedEventModel), cancellationToken);
            return DarwinResponse<TokenResponse>.Success(await _tokenService.CreateTokenAsync(appUser), 201);
        }
    }
}
