using Darwin.Contents.Core.AbstractServices;
using Darwin.Contents.Core.Dtos.Responses.Content;
using Darwin.Contents.Core.RequestModels.Contents;
using Darwin.Contents.Service.Common;
using Darwin.Shared.Dtos;
using FluentValidation;

namespace Darwin.Application.Features.Contents.Commands;

public static class UpdateContent
{
    public record Command(Guid Id, UpdateContentRequest Model) : ICommand<DarwinResponse<UpdatedContentResponse>>;

    public class CommandHandler(IContentService _contentService) : ICommandHandler<Command, DarwinResponse<UpdatedContentResponse>>
    {

        public async Task<DarwinResponse<UpdatedContentResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var updatedContentResponse= await _contentService.UpdateAsync(request.Id,request.Model);

            return DarwinResponse<UpdatedContentResponse>.Success(updatedContentResponse);
        }
    }
    public class UpdateContentCommandValidator : AbstractValidator<Command>
    {
        public UpdateContentCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
            RuleFor(x => x.Model.Name).NotEmpty().NotNull().Length(3, 64);
        }
    }
}