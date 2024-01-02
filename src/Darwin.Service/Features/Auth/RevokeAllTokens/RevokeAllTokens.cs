using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Model.Common;
using Darwin.Service.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Darwin.Service.Features.Auth.RevokeAllTokens
{
    public static class RevokeAllTokens
    {
        public record Command() : ICommand<DarwinResponse<NoContent>>;
        public class CommandHandler : ICommandHandler<Command, DarwinResponse<NoContent>>
        {
            private readonly UserManager<AppUser> _userManager;

            public CommandHandler(UserManager<AppUser> userManager)
            {
                _userManager = userManager;
            }

            public async Task<DarwinResponse<NoContent>> Handle(Command request, CancellationToken cancellationToken)
            {
                List<AppUser> users= await _userManager.Users.ToListAsync(cancellationToken);

                users.ForEach(async user =>
                {
                    user.RefreshToken = null;
                    await _userManager.UpdateAsync(user);
                });
                return DarwinResponse<NoContent>.Success();
            }
        }
    }
}