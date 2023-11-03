using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Model.Request.Users;
using Darwin.Model.Response.Users;
using Darwin.Service.Common;
using Darwin.Service.Notifications;
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

        public CommandHandler(UserManager<AppUser> userManager, IPublisher publisher)
        {
            _userManager = userManager;
            _publisher = publisher;
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
            var userCreatedEventModel=new UserCreatedMailModel(appUser.Email,"burasıConfirmLinki",appUser.UserName,appUser.CreatedAt);
            await _publisher.Publish(new UserCreatedEvent(userCreatedEventModel),cancellationToken);

            return DarwinResponse<CreatedUserResponse>.Success(appUser.Adapt<CreatedUserResponse>(), 201);
        }
    }
}
