using Darwin.Application.Common;
using Darwin.Application.Events.UserCreated;
using Darwin.Application.Helper;
using Darwin.Application.Services;
using Darwin.Domain.BaseDto;
using Darwin.Domain.Common;
using Darwin.Domain.RequestModels.Users;
using Darwin.Shared.Events;
using FluentValidation;
using MassTransit;
using MediatR;

namespace Darwin.Application.Features.Auth.Register;

public static class Register
{
    public record Command(RegisterRequest Model) : ICommand<DarwinResponse<NoContent>>;


    public class CommandHandler : ICommandHandler<Command, DarwinResponse<NoContent>>
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly MediatR.IPublisher _publisher;
        private readonly ILinkCreator _linkCreator;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public CommandHandler(IAuthService authService,
            ILinkCreator linkCreator,
            IUserService userService,
            ISendEndpointProvider sendEndpointProvider,
            MediatR.IPublisher publisher
            )
        {
            _authService = authService;
            _linkCreator = linkCreator;
            _userService = userService;
            _sendEndpointProvider = sendEndpointProvider;
            _publisher = publisher;
        }

        public async Task<DarwinResponse<NoContent>> Handle(Command request, CancellationToken cancellationToken)
        {
            var appUser= await _authService.RegisterAsync(request.Model);


            // !* Create Favorite List For new User
            await _publisher.Publish(new UserCreatedCreateFavoritePlaylistEvent(appUser.Id), cancellationToken);


            //Create confirmation link
            var confirmationToken = await _userService.GenerateEmailConfirmationTokenAsyncByUserIdAsync(appUser.Id);
            var confirmationUrl = await _linkCreator.CreateTokenMailUrl("ConfirmEmail", "User", appUser.Id, confirmationToken);


            var sendEndpoint=await _sendEndpointProvider.GetSendEndpoint(new System.Uri("queue:user-created-event-queue"));

            var userCreatedEvent=new UserCreatedEvent
            {
                Email = appUser.Email!,
                Name = appUser.Name,
                LastName = appUser.LastName,
                UserName = appUser.UserName!,
                PhoneNumber = appUser.PhoneNumber,
                CreatedDate = appUser.CreatedOnUtc,
                EmailConfirmationLink=confirmationUrl
            };
            await sendEndpoint.Send<UserCreatedEvent>(userCreatedEvent);
           
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
