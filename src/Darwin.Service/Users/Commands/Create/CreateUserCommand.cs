using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Model.Common;
using Darwin.Model.Request.Users;
using Darwin.Service.Common;
using Microsoft.AspNetCore.Identity;

namespace Darwin.Service.Users.Commands.Create;

public class CreateUserCommand : ICommand<DarwinResponse<NoContent>>
{
    public CreateUserRequest Model { get; }

    public CreateUserCommand(CreateUserRequest model)
    {
        Model = model;
    }

    public class Handler : ICommandHandler<CreateUserCommand, DarwinResponse<NoContent>>
    {
        private readonly UserManager<AppUser> _userManager;

        public Handler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<DarwinResponse<NoContent>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var appUser = new AppUser()
            {
                Email = request.Model.Email,
                UserName = Guid.NewGuid().ToString()
            };
            var createUserResult = await _userManager.CreateAsync(appUser, request.Model.Password);
            if (!createUserResult.Succeeded)
            {
                return DarwinResponse<NoContent>.Fail(createUserResult.Errors.Select(x => x.Description).ToList(), 400);
            }
            return DarwinResponse<NoContent>.Success(201);
        }
    }
}