using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Request.Moods;
using Darwin.Model.Response.Moods;
using Darwin.Service.Common;
using Darwin.Service.Services;
using FluentValidation;
using Mapster;

namespace Darwin.Service.Features.Moods.Commands;

public static class CreateMood
{
    public record Command(CreateMoodRequest Model) : ICommand<DarwinResponse<CreatedMoodResponse>>;
    public class CommandHandler : ICommandHandler<Command, DarwinResponse<CreatedMoodResponse>>
    {
        private readonly IGenericRepository<Mood> _repository;
        private readonly IFileService _fileService;

        public CommandHandler(IGenericRepository<Mood> repository, IFileService fileService)
        {
            _repository = repository;
            _fileService = fileService;
        }

        public async Task<DarwinResponse<CreatedMoodResponse>> Handle(Command request, CancellationToken cancellationToken)
        {

            //image upload returning url and add entity imageUrl

            var imageUrl= await _fileService.UploadImage(request.Model.ImageFile);

            Mood mood = request.Model.Adapt<Mood>();
            mood.ImageUrl = imageUrl;

            await _repository.CreateAsync(mood);

            return DarwinResponse<CreatedMoodResponse>.Success(mood.Adapt<CreatedMoodResponse>(), 201);
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
