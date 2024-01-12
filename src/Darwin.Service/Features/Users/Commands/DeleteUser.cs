using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Model.Common;
using Darwin.Service.Common;
using Darwin.Service.Helper;
using Microsoft.AspNetCore.Identity;

namespace Darwin.Service.Features.Users.Commands
{
    public static class DeleteUser
    {
        public record Command() : ICommand<DarwinResponse<NoContent>>;

        public class CommandHandler : ICommandHandler<Command, DarwinResponse<NoContent>>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly ICurrentUser _currentUser;

            public CommandHandler(UserManager<AppUser> userManager, ICurrentUser currentUser)
            {
                _userManager = userManager;
                _currentUser = currentUser;
            }

            public async Task<DarwinResponse<NoContent>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByIdAsync(_currentUser.GetUserId);

                if (user is null)
                    return DarwinResponse<NoContent>.Fail("User not found.", 404);

                var deleteResult = await _userManager.DeleteAsync(user);
                if (!deleteResult.Succeeded)
                    return DarwinResponse<NoContent>.Fail(deleteResult.Errors.Select(x => x.Description).ToList(), 500);

                return DarwinResponse<NoContent>.Success(204);
            }
        }
    }
}