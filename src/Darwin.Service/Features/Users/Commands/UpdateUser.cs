using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Model.Request.Users;
using Darwin.Model.Response.Users;
using Darwin.Service.Common;
using Darwin.Service.Helper;
using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace Darwin.Service.Features.Users.Commands;

public static class UpdateUser
{
    public record Command(UpdateUserRequest Model) : ICommand<DarwinResponse<GetUserResponse>>;

    public class CommandHandler : ICommandHandler<Command, DarwinResponse<GetUserResponse>>
    {
        private readonly ICurrentUser _currentUser;
        private readonly UserManager<AppUser> _userManager;

        public CommandHandler(ICurrentUser currentUser, UserManager<AppUser> userManager)
        {
            _currentUser = currentUser;
            _userManager = userManager;
        }

        public async Task<DarwinResponse<GetUserResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            AppUser? appUser = await _userManager.FindByIdAsync(_currentUser.GetUserId);

            if (appUser is null)
                return DarwinResponse<GetUserResponse>.Fail("User not found", 404);

            appUser.Name = request.Model.Name;
            appUser.LastName = request.Model.LastName;
            appUser.UserName = request.Model.UserName;
            appUser.BirthDate = request.Model.BirthDate;
            appUser.PhoneNumber = request.Model.PhoneNumber;

            var updateResult = await _userManager.UpdateAsync(appUser);
            if (updateResult.Errors.Any())
            {
                return DarwinResponse<GetUserResponse>.Fail(updateResult.Errors.Select(x => x.Description).ToList(), Convert.ToInt32(updateResult.Errors.Select(x => x.Code)));
            }
            return DarwinResponse<GetUserResponse>.Success(appUser.Adapt<GetUserResponse>());
        }
        public class UpdateUserCommandValidator : AbstractValidator<Command>
        {
            public UpdateUserCommandValidator()
            {
                RuleFor(x => x.Model.UserName)
                .NotNull().WithMessage("Username cannot null");
            }
        }
    }
}
