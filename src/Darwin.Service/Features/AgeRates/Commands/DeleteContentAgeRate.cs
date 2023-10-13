using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Model.Common;
using Darwin.Service.Common;
using FluentValidation;

namespace Darwin.Service.Features.AgeRates.Commands;

public static class DeleteContentAgeRate
{
    public record Command(Guid id) : ICommand<DarwinResponse<NoContent>>;

    public class CommandHandler : ICommandHandler<Command, DarwinResponse<NoContent>>
    {
        private readonly IGenericRepository<ContentAgeRate> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CommandHandler(IGenericRepository<ContentAgeRate> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DarwinResponse<NoContent>> Handle(Command request, CancellationToken cancellationToken)
        {

            var entity=await _repository.GetAsync(x=>x.Id==request.id);
            entity.IsActive = false;
            await _repository.UpdateAsync(entity);
            await _unitOfWork.CommitAsync();
            return DarwinResponse<NoContent>.Success(204);
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
