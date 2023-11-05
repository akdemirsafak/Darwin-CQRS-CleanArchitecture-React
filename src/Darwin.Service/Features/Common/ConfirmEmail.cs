using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Model.Common;
using Darwin.Service.Common;
using Microsoft.AspNetCore.Identity;

namespace Darwin.Service.Features.Common;

public static class ConfirmEmail
{
    public record Command(string userId, string token) : ICommand<DarwinResponse<NoContent>>;

    public class CommandHandler : ICommandHandler<Command, DarwinResponse<NoContent>>
    {
        private readonly UserManager<AppUser> _userManager;

        public CommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<DarwinResponse<NoContent>> Handle(Command request, CancellationToken cancellationToken)
        {
            var existUser= await _userManager.FindByIdAsync(request.userId);
            var result=await _userManager.ConfirmEmailAsync(existUser!, request.token);
            if (!result.Succeeded)
            {
                return DarwinResponse<NoContent>.Fail("Mail onaylanamadı.");
            }
            return DarwinResponse<NoContent>.Success();

        }
    }
}
