using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
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
                List<AppUser> users= await _userManager.Users.ToListAsync(cancellationToken);
                
                foreach(AppUser user in users)
                {
                    var userRefreshToken= await _userRefreshTokenRepository.GetAsync(x=>x.UserId==user.Id);
                    userRefreshToken.Code = null;
                }
                await _unitOfWork.CommitAsync();

                return DarwinResponse<NoContent>.Success();
            }
        }
    }
}
