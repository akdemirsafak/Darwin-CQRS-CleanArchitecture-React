using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Model.Response.AgeRates;
using Darwin.Service.Common;
using FluentValidation;

namespace Darwin.Service.Features.AgeRates.Commands;

public static class DeleteAgeRate
{
    public record Command(Guid id) : ICommand<DarwinResponse<DeletedAgeRateResponse>>;

    public class CommandHandler : ICommandHandler<Command, DarwinResponse<DeletedAgeRateResponse>>
    {
        private readonly IGenericRepository<AgeRate> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CommandHandler(IGenericRepository<AgeRate> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DarwinResponse<DeletedAgeRateResponse>> Handle(Command request, CancellationToken cancellationToken)
        {

            var entity=await _repository.GetAsync(x=>x.Id==request.id);
            entity.IsActive = false;
            await _repository.UpdateAsync(entity);
            await _unitOfWork.CommitAsync();
            return DarwinResponse<DeletedAgeRateResponse>.Success(204);
        }
    }
    public class DeleteContentAgeRateValidator : AbstractValidator<Command>
    {
        public DeleteContentAgeRateValidator()
        {
            RuleFor(x => x.id)
                .NotNull();
        }
    }
}
