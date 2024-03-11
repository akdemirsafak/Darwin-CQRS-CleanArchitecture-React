using Darwin.Application.Common;
using Darwin.Application.Events.UserCreated;
using Darwin.Application.Helper;
using Darwin.Application.Services;
using Darwin.Domain.BaseDto;
using Darwin.Domain.Common;
using Darwin.Domain.RequestModels.Users;
using FluentValidation;
using MediatR;

namespace Darwin.Application.Features.Auth.Register;

public static class Register
{
    public record Command(RegisterRequest Model) : ICommand<DarwinResponse<NoContent>>;


    public class CommandHandler : ICommandHandler<Command, DarwinResponse<NoContent>>
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IPublisher _publisher;
        private readonly ILinkCreator _linkCreator;

        public CommandHandler(IAuthService authService,
            IPublisher publisher,
            ILinkCreator linkCreator,
            IUserService userService)
        {
            _authService = authService;
            _publisher = publisher;
            _linkCreator = linkCreator;
            _userService = userService;

        }

        public async Task<DarwinResponse<NoContent>> Handle(Command request, CancellationToken cancellationToken)
        {
            var appUser= await _authService.RegisterAsync(request.Model);


            // !* Create Favorite List For new User
            await _publisher.Publish(new UserCreatedCreateFavoritePlaylistEvent(appUser.Id), cancellationToken);


            //Create confirmation link
            var confirmationToken = await _userService.GenerateEmailConfirmationTokenAsyncByUserIdAsync(appUser.Id);
            var confirmationUrl = await _linkCreator.CreateTokenMailUrl("ConfirmEmail", "User", appUser.Id, confirmationToken);



            //Send welcome message with confirmation link
            var userCreatedEventModel = new UserCreatedMailModel(appUser.Email!, confirmationUrl, appUser.CreatedOnUtc);
            await _publisher.Publish(new UserCreatedSendMailEvent(userCreatedEventModel));

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
