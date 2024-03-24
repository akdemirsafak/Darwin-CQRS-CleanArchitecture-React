using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Domain.BaseDto;
using Darwin.Domain.RequestModels.Users;
using Darwin.Domain.ResponseModels.Users;
using FluentValidation;

namespace Darwin.Application.Features.Users.Commands;

public static class UpdateUser
{
    public record Command(UpdateUserRequest Model) : ICommand<DarwinResponse<GetUserResponse>>;

    public class CommandHandler : ICommandHandler<Command, DarwinResponse<GetUserResponse>>
    {
        private readonly IUserService _userService;

        public CommandHandler(IUserService userService)
        {
            _userService = userService;
        }


        public async Task<DarwinResponse<GetUserResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var getUserResponse= await _userService.UpdateAsync(request.Model);
            return DarwinResponse<GetUserResponse>.Success(getUserResponse);
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
