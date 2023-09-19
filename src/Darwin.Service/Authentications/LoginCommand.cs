using AutoMapper;
using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Model.Common;
using Darwin.Model.Request.Authentications;
using Darwin.Service.Common;
using Microsoft.AspNetCore.Identity;

namespace Darwin.Service.Authentications;

public class LoginCommand:ICommand<DarwinResponse<NoContent>>
{
    public LoginRequest Model { get;}

    public LoginCommand(LoginRequest model)
    {
        Model = model;
    }

    public class Handler : ICommandHandler<LoginCommand, DarwinResponse<NoContent>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public Handler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<DarwinResponse<NoContent>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var existUser= await _userManager.FindByNameAsync(request.Model.UserName);
            if (existUser is null)
            {
                return DarwinResponse<NoContent>.Fail("User Not Found.", 400);
            }
            var loginResult= await _signInManager.CheckPasswordSignInAsync(existUser,request.Model.Password,true);
            if (loginResult.IsLockedOut)
            {
                return DarwinResponse<NoContent>.Fail("User locked.Try again 5 min.", 400);
            }
            if (!loginResult.Succeeded)
            {
                return DarwinResponse<NoContent>.Fail("Login Failed.", 400);
            }
            existUser.LastLogin = DateTime.UtcNow.Ticks;
            await _userManager.UpdateAsync(existUser);
            return DarwinResponse<NoContent>.Success(200);
        }
    }
}
