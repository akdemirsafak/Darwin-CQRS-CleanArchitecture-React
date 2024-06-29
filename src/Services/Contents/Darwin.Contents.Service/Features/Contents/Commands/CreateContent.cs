using Darwin.Contents.Core.AbstractServices;
using Darwin.Contents.Core.Dtos.Responses.Content;
using Darwin.Contents.Core.RequestModels.Contents;
using Darwin.Contents.Service.Common;
using Darwin.Shared.Dtos;
using Darwin.Shared.Dtos.Azure;
using Darwin.Shared.Utils;
using FluentValidation;

namespace Darwin.Application.Features.Contents.Commands;

public static class CreateContent
{
    public record Command(CreateContentRequest Model) : ICommand<DarwinResponse<NoContentDto>>;
    public class CommandHandler : ICommandHandler<Command, DarwinResponse<NoContentDto>>
    {
        private readonly IFileService _fileService;
        private readonly IContentService _contentService;

        public CommandHandler(
            IFileService fileService,
            IContentService contentService)
        {
            _fileService = fileService;
            _contentService = contentService;
        }

        public async Task<DarwinResponse<NoContentDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var uploadResponse=await _fileService.UploadAsync(request.Model.ImageFile, AzureContainerNames.Contents);
            BlobResponseDto uploadBlobResponse = uploadResponse.Data;
            string imageUrl = uploadBlobResponse.Blob.Url!;

            await _contentService.CreateAsync(request.Model, imageUrl);
            return DarwinResponse<NoContentDto>.Success(201);
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