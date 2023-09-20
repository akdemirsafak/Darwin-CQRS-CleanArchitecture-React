using AutoMapper;
using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Model.Common;
using Darwin.Model.Request.Authentications;
using Darwin.Service.Common;
using Darwin.Service.TokenOperations;
using Microsoft.AspNetCore.Identity;

namespace Darwin.Service.Authentications;

public class LoginCommand:ICommand<DarwinResponse<TokenResponse>>
{
    public LoginRequest Model { get;}

    public LoginCommand(LoginRequest model)
    {
        Model = model;
    }

    public class Handler : ICommandHandler<LoginCommand, DarwinResponse<TokenResponse>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        public Handler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<DarwinResponse<TokenResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var existUser= await _userManager.FindByNameAsync(request.Model.UserName);
            if (existUser is null)
            {
                return DarwinResponse<TokenResponse>.Fail("User Not Found.", 400);
            }
            var loginResult= await _signInManager.CheckPasswordSignInAsync(existUser,request.Model.Password,true);
            if (loginResult.IsLockedOut)
            {
                return DarwinResponse<TokenResponse>.Fail("User locked.Try again 5 min.", 400);
            }
            if (!loginResult.Succeeded)
            {
                return DarwinResponse<TokenResponse>.Fail("Login Failed.", 400);
            }
            existUser.LastLogin = DateTime.UtcNow.Ticks;
            var updateResult=await _userManager.UpdateAsync(existUser);
            if(!updateResult.Succeeded)
            {
                return DarwinResponse<TokenResponse>.Fail("Login cannot updated.", 500); //Refactor
            }
            return DarwinResponse<TokenResponse>.Success(_tokenService.CreateToken(existUser),200);
        }
    }
}
