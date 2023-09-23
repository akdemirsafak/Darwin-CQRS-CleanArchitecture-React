using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Model.Request.Authentications;
using Darwin.Service.Common;
using Darwin.Service.TokenOperations;
using Microsoft.AspNetCore.Identity;

namespace Darwin.Service.Authentications;

public class RegisterCommand : ICommand<DarwinResponse<TokenResponse>>
{
    public RegisterRequest Model { get; }

    public RegisterCommand(RegisterRequest model)
    {
        Model = model;
    }

    public class Handler : ICommandHandler<RegisterCommand, DarwinResponse<TokenResponse>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public Handler(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<DarwinResponse<TokenResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (request.Model.Password != request.Model.PasswordAgain)
            {
                return DarwinResponse<TokenResponse>.Fail("Passwords do not match.", 400);
            }
            AppUser newUser=new AppUser{UserName=request.Model.UserName};
            var registerResult= await _userManager.CreateAsync(newUser,request.Model.Password);
            if (!registerResult.Succeeded)
            {
                return DarwinResponse<TokenResponse>.Fail(registerResult.Errors.Select(x => x.Description).ToList());
            }
            return DarwinResponse<TokenResponse>.Success(_tokenService.CreateToken(newUser), 201);
        }
    }
}
