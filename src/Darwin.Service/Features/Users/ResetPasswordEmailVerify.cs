using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Model.Common;
using Darwin.Model.Request.Users;
using Darwin.Service.Common;
using Darwin.Service.EmailServices;
using Darwin.Service.Helper;
using Microsoft.AspNetCore.Identity;

namespace Darwin.Service.Features.Users;

public static class ResetPasswordEmailVerify
{
    public record Command(ResetPasswordRequest Model, string userId, string Token) : ICommand<DarwinResponse<NoContent>>;

    public class CommandHanler : ICommandHandler<Command, DarwinResponse<NoContent>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ILinkCreator _linkCreator;
        private readonly IEmailService _emailService;
        public CommandHanler(UserManager<AppUser> userManager,
            ILinkCreator linkCreator,
            IEmailService emailService)
        {
            _userManager = userManager;
            _linkCreator = linkCreator;
            _emailService = emailService;
        }

        public async Task<DarwinResponse<NoContent>> Handle(Command request, CancellationToken cancellationToken)
        {
            AppUser? user= await _userManager.FindByIdAsync(request.userId);
            var result=await _userManager.ResetPasswordAsync(user,request.Token,request.Model.Password);

            await _userManager.UpdateSecurityStampAsync(user);

            if (!result.Succeeded)
                return DarwinResponse<NoContent>.Fail("Şifre güncellenemedi.");

            return DarwinResponse<NoContent>.Success();
        }
    }
}
