using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Request.Moods;
using Darwin.Model.Response.Moods;
using Darwin.Service.Common;
using FluentValidation;
using Mapster;

namespace Darwin.Service.Features.Moods.Commands;

public static class UpdateMood
{
    public record Command(Guid Id, UpdateMoodRequest Model) : ICommand<DarwinResponse<UpdatedMoodResponse>>;

    public class CommandHandler(IGenericRepository<Mood> _repository) 
        : ICommandHandler<Command, DarwinResponse<UpdatedMoodResponse>>
    {
       
        public async Task<DarwinResponse<UpdatedMoodResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var existMood = await _repository.GetAsync(x => x.Id == request.Id);
            if (existMood is null)
                return DarwinResponse<UpdatedMoodResponse>.Fail("");
            existMood.ImageUrl = request.Model.ImageUrl;
            existMood.Name = request.Model.Name;
            existMood.IsUsable = request.Model.IsUsable;

            await _repository.UpdateAsync(existMood);

            return DarwinResponse<UpdatedMoodResponse>.Success(existMood.Adapt<UpdatedMoodResponse>());
        }
    }
    public class UpdateMoodCommandValidator : AbstractValidator<Command>
    {
        public UpdateMoodCommandValidator()
        {
            RuleFor(x => x.Model.Name)
                .NotNull()
                .NotEmpty()
                .Length(3, 64);
        }
    }
}

