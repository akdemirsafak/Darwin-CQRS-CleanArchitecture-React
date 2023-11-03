using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Model.Common;
using Darwin.Service.Common;
using Microsoft.AspNetCore.Identity;

namespace Darwin.Service.Features.Roles;

public static class CreateRole
{
    public record Command(string role): ICommand<DarwinResponse<NoContent>>;

    public class CommandHandler : ICommandHandler<Command, DarwinResponse<NoContent>>
    {
        private readonly RoleManager<AppRole> _roleManager;

        public CommandHandler(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<DarwinResponse<NoContent>> Handle(Command request, CancellationToken cancellationToken)
        {
            var result = await _roleManager.CreateAsync(new AppRole() { Name=request.role});
            if(!result.Succeeded)
            {
                return DarwinResponse<NoContent>.Fail(result.Errors.First().Description,500);
            }
            return DarwinResponse<NoContent>.Success(204);
        }
    }
}
