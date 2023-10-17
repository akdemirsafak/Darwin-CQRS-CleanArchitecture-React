using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Model.Request.Users;
using Darwin.Model.Response.Users;
using Darwin.Service.Common;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace Darwin.Service.Features.Users.Commands;

public static class CreateUser
{
    public record Command(CreateUserRequest Model) : ICommand<DarwinResponse<CreatedUserResponse>>;

    public class CommandHandler : ICommandHandler<Command, DarwinResponse<CreatedUserResponse>>
    {
        private readonly UserManager<AppUser> _userManager;

        public CommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<DarwinResponse<CreatedUserResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var appUser = request.Model.Adapt<AppUser>();
            appUser.BirthDate= DateTime.UtcNow.AddYears(-15);
            var createUserResult = await _userManager.CreateAsync(appUser, request.Model.Password);
            if (!createUserResult.Succeeded)
            {
                return DarwinResponse<CreatedUserResponse>.Fail(createUserResult.Errors.Select(x => x.Description).ToList(), 400);
            }
            return DarwinResponse<CreatedUserResponse>.Success(appUser.Adapt<CreatedUserResponse>(), 201);
        }
    }
}
