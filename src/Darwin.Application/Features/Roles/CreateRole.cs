using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Share.Dtos;
using FluentValidation;

namespace Darwin.Application.Features.Roles;

public static class CreateRole
{
    public record Command(string role) : ICommand<DarwinResponse<NoContentDto>>;

    public class CommandHandler : ICommandHandler<Command, DarwinResponse<NoContentDto>>
    {
        private readonly IRoleService _roleService;

        public CommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<DarwinResponse<NoContentDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            await _roleService.CreateAsync(request.role);
            return DarwinResponse<NoContentDto>.Success(204);
        }
    }
    public class CreateRoleCommandValidator : AbstractValidator<Command>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(x => x.role)
                .NotEmpty()
                .NotNull()
                .Length(3, 15);
        }
    }
}