using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Model.Common;
using Darwin.Model.Request.ContentAgeRates;
using Darwin.Service.Common;
using FluentValidation;
using Mapster;

namespace Darwin.Service.Features.AgeRates.Commands;

public static class CreateContentAgeRate
{
    public record Command(CreateContentAgeRequest Model):ICommand<DarwinResponse<NoContent>>;

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
            var entity = request.Model.Adapt<ContentAgeRate>();
            await _repository.CreateAsync(entity);
            await _unitOfWork.CommitAsync();
            return DarwinResponse<NoContent>.Success(201);
        }
    }
    public class CreateContentAgeRateValidator : AbstractValidator<Command>
    {
        public CreateContentAgeRateValidator()
        {
            RuleFor(x => x.Model.Name)
                .NotEmpty()
                .NotNull()
                .Length(3, 64);
            RuleFor(x => x.Model.Rate)
                .NotNull()
                .GreaterThanOrEqualTo(1);
        }
    }
}
