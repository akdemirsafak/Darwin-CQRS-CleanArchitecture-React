using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Model.Common;
using Darwin.Service.Common;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Darwin.Service.Features.Auth.RevokeToken;

public static class RevokeToken
{
    public record Command(string Email):ICommand<DarwinResponse<NoContent>>;
    public class CommandHandler : ICommandHandler<Command, DarwinResponse<NoContent>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CommandHandler(UserManager<AppUser> userManager,
            IGenericRepository<UserRefreshToken> userRefreshTokenRepository,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _userRefreshTokenRepository = userRefreshTokenRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DarwinResponse<NoContent>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user= await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                return DarwinResponse<NoContent>.Fail("User not found.");
            }
            var userRefreshToken= await _userRefreshTokenRepository.GetAsync(x=>x.UserId==user.Id);
            userRefreshToken.Code = null;
            await _unitOfWork.CommitAsync();
            return DarwinResponse<NoContent>.Success();
        }
    }
    public class RevokeTokenCommandValidator: AbstractValidator<Command>
    {
        public RevokeTokenCommandValidator()
        {
            RuleFor(x=>x.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }

}
