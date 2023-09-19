using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Model.Common;
using Darwin.Model.Request.Authentications;
using Darwin.Service.Common;
using Microsoft.AspNetCore.Identity;

namespace Darwin.Service.Authentications;

public class RegisterCommand : ICommand<DarwinResponse<NoContent>>
{
    public RegisterRequest Model { get; }

    public RegisterCommand(RegisterRequest model)
    {
        Model = model;
    }

    public class Handler : ICommandHandler<RegisterCommand, DarwinResponse<NoContent>>
    {
        private readonly UserManager<AppUser> _userManager;

        public Handler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<DarwinResponse<NoContent>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var hasUser= await _userManager.FindByNameAsync(request.Model.UserName);
            if(hasUser is not null)
            {
                return DarwinResponse<NoContent>.Fail("Already exist Username.", 400);
            }
            if (request.Model.Password != request.Model.PasswordAgain)
            {
                return DarwinResponse<NoContent>.Fail("Passwords do not match.", 400);
            }
            var registerResult= await _userManager.CreateAsync(new AppUser{UserName=request.Model.UserName},request.Model.Password);
            if (!registerResult.Succeeded)
            {
                return DarwinResponse<NoContent>.Fail(new List<string> { $"Email veya şifre yanlış.", $"Başarısız giriş sayısı : {registerResult.Errors.SelectMany(x=>x.Description).ToList()}"});
            }
            return DarwinResponse<NoContent>.Success(201);
        }
    }
}
