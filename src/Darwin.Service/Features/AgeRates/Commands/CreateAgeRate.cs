using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Model.Request.ContentAgeRates;
using Darwin.Model.Response.AgeRates;
using Darwin.Service.Common;
using FluentValidation;
using Mapster;

namespace Darwin.Service.Features.AgeRates.Commands;

public static class CreateAgeRate
{
    public record Command(CreateAgeRequest Model) : ICommand<DarwinResponse<CreatedAgeRateResponse>>;

    public class CommandHandler : ICommandHandler<Command, DarwinResponse<CreatedAgeRateResponse>>
    {
        private readonly IGenericRepository<AgeRate> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CommandHandler(IGenericRepository<AgeRate> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DarwinResponse<CreatedAgeRateResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = request.Model.Adapt<AgeRate>();
            await _repository.CreateAsync(entity);
            await _unitOfWork.CommitAsync();
            return DarwinResponse<CreatedAgeRateResponse>.Success(entity.Adapt<CreatedAgeRateResponse>(), 201);
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
