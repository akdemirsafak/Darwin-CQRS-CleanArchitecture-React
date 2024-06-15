using Darwin.Contents.Core.AbstractServices;
using Darwin.Contents.Core.RequestModels.Moods;
using Darwin.Contents.Service.Common;
using Darwin.Shared.Dtos;
using Darwin.Shared.Dtos.Azure;
using Darwin.Shared.Utils;
using FluentValidation;

namespace Darwin.Application.Features.Moods.Commands;

public static class CreateMood
{
    public record Command(CreateMoodRequest Model) : ICommand<DarwinResponse<NoContentDto>>;
    public class CommandHandler : ICommandHandler<Command, DarwinResponse<NoContentDto>>
    {
        private readonly IMoodService _moodService;
        private readonly IFileService _fileService;
        public CommandHandler(IFileService fileService, IMoodService moodService)
        {
            _fileService = fileService;
            _moodService = moodService;
        }

        public async Task<DarwinResponse<NoContentDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var uploadMoodResponse=await _fileService.UploadAsync(request.Model.ImageFile, AzureContainerNames.Moods);
            BlobResponseDto responseData = uploadMoodResponse.Data; //Content
            string imageUrl = responseData.Blob.Url;

            await _moodService.CreateAsync(request.Model.Name, imageUrl);

            return DarwinResponse<NoContentDto>.Success(201);
        }
    }
    public class CreateMoodCommandValidator : AbstractValidator<Command>
    {
        public CreateMoodCommandValidator()
        {
            RuleFor(x => x.Model.Name)
           .NotNull()
           .NotEmpty()
           .Length(3, 32);
        }
    }
}
