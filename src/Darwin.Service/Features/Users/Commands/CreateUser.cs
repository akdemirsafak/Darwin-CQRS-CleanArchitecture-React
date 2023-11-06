using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Model.Request.Users;
using Darwin.Model.Response.Users;
using Darwin.Service.Common;
using Darwin.Service.Helper;
using Darwin.Service.Notifications.UserCreated;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Darwin.Service.Features.Users.Commands;

public static class CreateUser
{
    public record Command(CreateUserRequest Model) : ICommand<DarwinResponse<CreatedUserResponse>>;

    public class CommandHandler : ICommandHandler<Command, DarwinResponse<CreatedUserResponse>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IPublisher _publisher;
        private readonly ILinkCreator _linkCreator;

        public CommandHandler(UserManager<AppUser> userManager, IPublisher publisher, ILinkCreator linkCreator)
        {
            _userManager = userManager;
            _publisher = publisher;
            _linkCreator = linkCreator;
        }

        public async Task<DarwinResponse<CreatedUserResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var appUser = request.Model.Adapt<AppUser>();
            appUser.BirthDate = DateTime.UtcNow.AddYears(-15);
            appUser.CreatedAt = DateTime.UtcNow;
            var createUserResult = await _userManager.CreateAsync(appUser, request.Model.Password);
            if (!createUserResult.Succeeded)
            {
                return DarwinResponse<CreatedUserResponse>.Fail(createUserResult.Errors.Select(x => x.Description).ToList(), 400);
            }

            // !* Create Favorite List For new User
            await _publisher.Publish(new UserCreatedCreateFavoritePlaylistEvent(appUser.Id), cancellationToken);

            //Create confirmation link
            var confirmationToken=await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
            var confirmationUrl= await _linkCreator.CreateTokenMailUrl("ConfirmEmail","Authentication",appUser.Id,confirmationToken);

            //Send welcome message with confirmation link
            var userCreatedEventModel=new UserCreatedMailModel(appUser.Email!,confirmationUrl!,appUser.UserName!,appUser.CreatedAt);
            await _publisher.Publish(new UserCreatedSendMailEvent(userCreatedEventModel), cancellationToken);

            return DarwinResponse<CreatedUserResponse>.Success(appUser.Adapt<CreatedUserResponse>(), 201);
        }
    }
}
