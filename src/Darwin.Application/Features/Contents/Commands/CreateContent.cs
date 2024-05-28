using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Domain.RequestModels.Contents;
using Darwin.Domain.ResponseModels.Contents;
using Darwin.Share.Dtos;
using Darwin.Shared.Dtos.Azure;
using Darwin.Shared.Utils;
using FluentValidation;

namespace Darwin.Application.Features.Contents.Commands;

public static class CreateContent
{
    public record Command(CreateContentRequest Model) : ICommand<DarwinResponse<CreatedContentResponse>>;
    public class CommandHandler : ICommandHandler<Command, DarwinResponse<CreatedContentResponse>>
    {

        private readonly IContentService _contentService;
        private readonly IAzureBlobStorageService _azureBlobStorageService;

        public CommandHandler(IContentService contentService,
            IAzureBlobStorageService azureBlobStorageService)
        {
            _contentService = contentService;
            _azureBlobStorageService = azureBlobStorageService;
        }

        public async Task<DarwinResponse<CreatedContentResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var uploadResponse=await _azureBlobStorageService.UploadAsync(request.Model.ImageFile, AzureContainerNames.Contents);
            BlobResponseDto uploadBlobResponse =uploadResponse.Data;
            string imageUrl = uploadBlobResponse.Blob.Url;

            return DarwinResponse<CreatedContentResponse>.Success(await _contentService.CreateAsync(request.Model, imageUrl), 201);
        }
    }
    public class CreateContentCommandValidator : AbstractValidator<Command>
    {
        public CreateContentCommandValidator()
        {
            RuleFor(x => x.Model.Name).NotEmpty().NotNull().Length(3, 64);
            RuleFor(x => x.Model.SelectedCategories).NotNull();
            RuleFor(x => x.Model.SelectedMoods).NotNull();
        }
    }
}