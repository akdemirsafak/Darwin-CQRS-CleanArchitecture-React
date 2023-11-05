using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Model.Request.Users;
using Darwin.Model.Response.Users;
using Darwin.Service.Common;
using Darwin.Service.Notifications.UserCreated;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;

namespace Darwin.Service.Features.Users.Commands;

public static class CreateUser
{
    public record Command(CreateUserRequest Model) : ICommand<DarwinResponse<CreatedUserResponse>>;

    public class CommandHandler : ICommandHandler<Command, DarwinResponse<CreatedUserResponse>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IPublisher _publisher;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LinkGenerator _linkGenerator;

        public CommandHandler(UserManager<AppUser> userManager, IPublisher publisher, IHttpContextAccessor httpContextAccessor, LinkGenerator linkGenerator)
        {
            _userManager = userManager;
            _publisher = publisher;
            _httpContextAccessor = httpContextAccessor;
            _linkGenerator = linkGenerator;
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

            var confirmationToken=await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
            var requestScheme=_httpContextAccessor.HttpContext!.Request.Scheme;
            var apiHost=_httpContextAccessor.HttpContext.Request.Host;
            var confirmationUrl=_linkGenerator.GetUriByAction("ConfirmEmail", "Authentication", new { userId = appUser.Id, token = confirmationToken }, requestScheme, apiHost);

            await _publisher.Publish(new UserCreatedCreateFavoritePlaylistEvent(appUser.Id), cancellationToken);

            var userCreatedEventModel=new UserCreatedMailModel(appUser.Email!,confirmationUrl!,appUser.UserName!,appUser.CreatedAt);
            await _publisher.Publish(new UserCreatedSendMailEvent(userCreatedEventModel), cancellationToken);

            return DarwinResponse<CreatedUserResponse>.Success(appUser.Adapt<CreatedUserResponse>(), 201);
        }
    }
}
