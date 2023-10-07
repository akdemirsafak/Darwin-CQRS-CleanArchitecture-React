using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Model.Request.Moods;
using Darwin.Model.Response.Moods;
using Darwin.Service.Common;
using FluentValidation;
using Mapster;

namespace Darwin.Service.Features.Moods.Commands;

public static class CreateMood
{
    public record Command(CreateMoodRequest Model) : ICommand<DarwinResponse<CreatedMoodResponse>>;

    public class CommandHandler : ICommandHandler<Command, DarwinResponse<CreatedMoodResponse>>
    {
        private readonly IGenericRepository<Mood> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CommandHandler(IGenericRepository<Mood> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DarwinResponse<CreatedMoodResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var existMood = await _repository.GetAsync(x => x.Name.ToLower().Trim() == request.Model.Name.ToLower().Trim());
            if (existMood is not null)
            {
                return DarwinResponse<CreatedMoodResponse>.Fail("Allready exist.");
            }
            Mood mood = request.Model.Adapt<Mood>();
            await _repository.CreateAsync(mood);
            await _unitOfWork.CommitAsync();
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
