using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Model.Request.Authentications;
using Darwin.Service.Common;
using Darwin.Service.Notifications.UserCreated;
using Darwin.Service.TokenOperations;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;

namespace Darwin.Service.Features.Authentications;

public static class Register
{
    public record Command(RegisterRequest Model) : ICommand<DarwinResponse<TokenResponse>>;


    public class CommandHandler : ICommandHandler<Command, DarwinResponse<TokenResponse>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IPublisher _publisher;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LinkGenerator _linkGenerator;
        public CommandHandler(UserManager<AppUser> userManager, ITokenService tokenService, IPublisher publisher, IHttpContextAccessor httpContextAccessor, LinkGenerator linkGenerator)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _publisher = publisher;
            _httpContextAccessor = httpContextAccessor;
            _linkGenerator = linkGenerator;
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


            var confirmationToken=await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
            var requestScheme=_httpContextAccessor.HttpContext!.Request.Scheme;
            var apiHost=_httpContextAccessor.HttpContext.Request.Host;
            var confirmationUrl=_linkGenerator.GetUriByAction("ConfirmEmail", "Authentication", new { userId = appUser.Id, token = confirmationToken }, requestScheme, apiHost);



            await _publisher.Publish(new UserCreatedCreateFavoritePlaylistEvent(appUser.Id), cancellationToken);
            var userCreatedEventModel=new UserCreatedMailModel(appUser.Email!,confirmationUrl,appUser.UserName!,appUser.CreatedAt);
            await _publisher.Publish(new UserCreatedSendMailEvent(userCreatedEventModel), cancellationToken);

            return DarwinResponse<TokenResponse>.Success(await _tokenService.CreateTokenAsync(appUser), 201);
        }
    }
}
