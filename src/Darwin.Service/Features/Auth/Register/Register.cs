using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Model.Common;
using Darwin.Model.Request.Users;
using Darwin.Service.Common;
using Darwin.Service.Events.UserCreated;
using Darwin.Service.Helper;
using Darwin.Service.TokenOperations;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Darwin.Service.Features.Auth.Register;

public static class Register
{
    public record Command(RegisterRequest Model) : ICommand<DarwinResponse<NoContent>>;


    public class CommandHandler : ICommandHandler<Command, DarwinResponse<NoContent>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly ITokenService _tokenService;
        private readonly IPublisher _publisher;
        private readonly ILinkCreator _linkCreator;

        public CommandHandler(UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            ITokenService tokenService,
            IPublisher publisher,
            ILinkCreator linkCreator)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _publisher = publisher;
            _linkCreator = linkCreator;
        }

        public async Task<DarwinResponse<NoContent>> Handle(Command request, CancellationToken cancellationToken)
        {
            AppUser appUser = new()
            {
                Email=request.Model.Email,
                UserName=request.Model.Email,
                SecurityStamp=Guid.NewGuid().ToString()
            };
            var registerResult = await _userManager.CreateAsync(appUser, request.Model.Password);

            if (!registerResult.Succeeded)
                return DarwinResponse<NoContent>.Fail(registerResult.Errors.Select(x => x.Description).ToList());

            //Kullanıcının bilgileri değişince security stamp'i de değiştirelim ki mevcut tokenla uyuşmasın ve tekrar giriş yapılması gereksin.
            //Telefon ve bilgisayarda açık aynı hesap olduğunu düşünürsek herhangi bir cihazda işlem yapılırsa diğerinden tekrar giriş yapılmasını gerektiren durumu sağlar.  


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


            // !* Create Favorite List For new User
            await _publisher.Publish(new UserCreatedCreateFavoritePlaylistEvent(appUser.Id), cancellationToken);


            //Create confirmation link
            var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
            var confirmationUrl = await _linkCreator.CreateTokenMailUrl("ConfirmEmail", "Auth", appUser.Id, confirmationToken);

            //Send welcome message with confirmation link
            var userCreatedEventModel = new UserCreatedMailModel(appUser.Email!, confirmationUrl, appUser.CreatedOnUtc);
            await _publisher.Publish(new UserCreatedSendMailEvent(userCreatedEventModel), cancellationToken);

            return DarwinResponse<NoContent>.Success(201);
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
            .Equal(x => x.Model.Password)
            .WithName("ConfirmPassword");
        }
    }
}
