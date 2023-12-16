using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Model.Request.Authentications;
using Darwin.Service.Common;
using Darwin.Service.Helper;
using Darwin.Service.Events.UserCreated;
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
        private readonly ILinkCreator _linkCreator;

        public CommandHandler(UserManager<AppUser> userManager, ITokenService tokenService, IPublisher publisher, ILinkCreator linkCreator)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _publisher = publisher;
            _linkCreator = linkCreator;
        }

        public async Task<DarwinResponse<TokenResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var appUser = request.Model.Adapt<AppUser>();
            appUser.UserName = Guid.NewGuid().ToString();
            var registerResult = await _userManager.CreateAsync(appUser, request.Model.Password);
            if (!registerResult.Succeeded)
            {
                return DarwinResponse<TokenResponse>.Fail(registerResult.Errors.Select(x => x.Description).ToList());
            }

            // !* Create Favorite List For new User
            await _publisher.Publish(new UserCreatedCreateFavoritePlaylistEvent(appUser.Id), cancellationToken);


            //Create confirmation link
            var confirmationToken=await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
            var confirmationUrl= await _linkCreator.CreateTokenMailUrl("ConfirmEmail","Authentication",appUser.Id,confirmationToken);

            //Send welcome message with confirmation link
            var userCreatedEventModel=new UserCreatedMailModel(appUser.Email!,confirmationUrl,appUser.CreatedOnUtc);
            await _publisher.Publish(new UserCreatedSendMailEvent(userCreatedEventModel), cancellationToken);

            return DarwinResponse<TokenResponse>.Success(await _tokenService.CreateTokenAsync(appUser), 201);
        }
    }
}
