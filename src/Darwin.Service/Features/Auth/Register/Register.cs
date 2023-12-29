using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Model.Request.Users;
using Darwin.Service.Common;
using Darwin.Service.Events.UserCreated;
using Darwin.Service.Helper;
using Darwin.Service.TokenOperations;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Darwin.Service.Features.Auth.Register;

public static class Register
{
    public record Command(RegisterRequest Model) : ICommand<DarwinResponse<TokenResponse>>;


    public class CommandHandler : ICommandHandler<Command, DarwinResponse<TokenResponse>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly ITokenService _tokenService;
        private readonly IPublisher _publisher;
        private readonly ILinkCreator _linkCreator;
        private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CommandHandler(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, ITokenService tokenService, IPublisher publisher, ILinkCreator linkCreator, IGenericRepository<UserRefreshToken> userRefreshTokenRepository, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _publisher = publisher;
            _linkCreator = linkCreator;
            _userRefreshTokenRepository = userRefreshTokenRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DarwinResponse<TokenResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var appUser = request.Model.Adapt<AppUser>();
            appUser.UserName = request.Model.Email;
            appUser.SecurityStamp = Guid.NewGuid().ToString(); //Kullanıcının bilgileri değişince security stamp'i de değiştirelim ki mevcut tokenla uyuşmasın ve tekrar giriş yapılması gereksin.
                                                               //Telefon ve bilgisayarda açık aynı hesap olduğunu düşünürsek herhangi bir cihazda işlem yapılırsa diğerinden tekrar giriş yapılmasını gerektiren durumu sağlar.  

            var registerResult = await _userManager.CreateAsync(appUser, request.Model.Password);
            if (!registerResult.Succeeded)
            {
                return DarwinResponse<TokenResponse>.Fail(registerResult.Errors.Select(x => x.Description).ToList());
            }

            var existRole= await _roleManager.RoleExistsAsync("User");
            if (!existRole) //Yoksa oluştur.
            {
                    var createdRoleResult=await _roleManager.CreateAsync(new AppRole() 
                    { 
                        Name = "User", 
                        ConcurrencyStamp = Guid.NewGuid().ToString() 
                    });

            }
            await _userManager.AddToRoleAsync(appUser, "User");


            var token = await _tokenService.CreateTokenAsync(appUser);

            //User'ın refresh token'ı yoksa

            var newRefreshToken = new UserRefreshToken()
            {
                UserId = appUser.Id,
                Code = token.RefreshToken,
                Expiration = token.RefreshTokenExpiration
            };
            await _userRefreshTokenRepository.CreateAsync(newRefreshToken);

            await _unitOfWork.CommitAsync();

            // !* Create Favorite List For new User
            await _publisher.Publish(new UserCreatedCreateFavoritePlaylistEvent(appUser.Id), cancellationToken);


            ////Create confirmation link
            //var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
            //var confirmationUrl = await _linkCreator.CreateTokenMailUrl("ConfirmEmail", "Authentication", appUser.Id, confirmationToken);

            ////Send welcome message with confirmation link
            //var userCreatedEventModel = new UserCreatedMailModel(appUser.Email!, confirmationUrl, appUser.CreatedOnUtc);
            //await _publisher.Publish(new UserCreatedSendMailEvent(userCreatedEventModel), cancellationToken);

            return DarwinResponse<TokenResponse>.Success(token, 201);
        }
    }
    public class RegisterCommandValidator : AbstractValidator<Command>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Model.Email)
                .EmailAddress()
                .NotEmpty()
                .WithName("Email Address");

            RuleFor(x => x.Model.Password)
            .NotEmpty()
            .WithName("Password");

            RuleFor(x => x.Model.ConfirmPassword)
            .NotEmpty()
            .Equal(x=>x.Model.Password)
            .WithName("ConfirmPassword");
        }
    }
}
