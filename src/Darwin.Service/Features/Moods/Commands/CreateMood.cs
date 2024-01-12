using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Request.Moods;
using Darwin.Model.Response.Moods;
using Darwin.Service.Common;
using FluentValidation;
using Mapster;

namespace Darwin.Service.Features.Moods.Commands;


public static class CreateMood
{
    public record Command(CreateMoodRequest Model) : ICommand<DarwinResponse<CreatedMoodResponse>>;
    public class CommandHandler(IGenericRepository<Mood> _repository) : ICommandHandler<Command, DarwinResponse<CreatedMoodResponse>>
    {

        public async Task<DarwinResponse<CreatedMoodResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var existMood = await _repository.GetAsync(x => x.Name.ToLower().Trim() == request.Model.Name.ToLower().Trim());

            if (existMood is not null)
                return DarwinResponse<CreatedMoodResponse>.Fail("Allready exist.");

            Mood mood = request.Model.Adapt<Mood>();
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
