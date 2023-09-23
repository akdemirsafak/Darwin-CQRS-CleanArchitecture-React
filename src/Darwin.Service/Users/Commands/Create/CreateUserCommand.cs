using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Model.Common;
using Darwin.Model.Request.Users;
using Darwin.Model.Response.Users;
using Darwin.Service.Common;
using Darwin.Service.Helpers;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace Darwin.Service.Users.Commands.Create;

public class CreateUserCommand : ICommand<DarwinResponse<CreatedUserResponse>>
{
    public CreateUserRequest Model { get; }

    public CreateUserCommand(CreateUserRequest model)
    {
        Model = model;
    }

    public class Handler : ICommandHandler<CreateUserCommand, DarwinResponse<CreatedUserResponse>>
    {
        private readonly UserManager<AppUser> _userManager;

        public Handler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<DarwinResponse<CreatedUserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var appUser = new AppUser()
            {
                Email = request.Model.Email,
                UserName = UserNameGeneratorThanEmail.Generate(request.Model.Email)
            };
            var createUserResult = await _userManager.CreateAsync(appUser, request.Model.Password);
            if (!createUserResult.Succeeded)
            {
                return DarwinResponse<CreatedUserResponse>.Fail(createUserResult.Errors.Select(x => x.Description).ToList(), 400);
            }
            return DarwinResponse<CreatedUserResponse>.Success(appUser.Adapt<CreatedUserResponse>(),201);
        }
    }
}