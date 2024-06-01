using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Domain.RequestModels.Moods;
using Darwin.Model.Response.Moods;
using Darwin.Shared.Dtos;
using Darwin.Shared.Dtos.Azure;
using Darwin.Shared.Utils;
using FluentValidation;

namespace Darwin.Application.Features.Moods.Commands;

public static class CreateMood
{
    public record Command(CreateMoodRequest Model) : ICommand<DarwinResponse<CreatedMoodResponse>>;
    public class CommandHandler : ICommandHandler<Command, DarwinResponse<CreatedMoodResponse>>
    {
        private readonly IMoodService _moodService;
        private readonly IAzureBlobStorageService _azureBlobStorageService;

        public CommandHandler(IMoodService moodService,
            IAzureBlobStorageService azureBlobStorageService)
        {
            _moodService = moodService;
            _azureBlobStorageService = azureBlobStorageService;
        }

        public async Task<DarwinResponse<CreatedMoodResponse>> Handle(Command request, CancellationToken cancellationToken)
        {

            //image upload returning url and add entity imageUrl

            //var imageUrl= await _fileService.UploadImage(request.Model.ImageFile);

            var uploadMoodResponse=await _azureBlobStorageService.UploadAsync(request.Model.ImageFile, AzureContainerNames.Moods);
            BlobResponseDto responseData = uploadMoodResponse.Data; //Content
            string imageUrl = responseData.Blob.Url;
            var createdMoodResponse=await _moodService.CreateAsync(request.Model.Name,request.Model.IsUsable,imageUrl);

            return DarwinResponse<CreatedMoodResponse>.Success(createdMoodResponse, 201);
        }
    }
    public class CreateMoodCommandValidator : AbstractValidator<Command>
    {
        public CreateMoodCommandValidator()
        {
            RuleFor(x => x.Model.Name)
           .NotNull()
           .NotEmpty()
           .Length(3, 64);
        }
    }
}
