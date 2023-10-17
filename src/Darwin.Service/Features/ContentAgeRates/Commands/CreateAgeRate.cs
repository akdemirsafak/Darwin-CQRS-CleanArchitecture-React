using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Model.Request.AgeRates;
using Darwin.Model.Response.AgeRates;
using Darwin.Service.Common;
using FluentValidation;
using Mapster;

namespace Darwin.Service.Features.ContentAgeRates.Commands;

public static class CreateAgeRate
{
    public record Command(CreateAgeRateRequest Model) : ICommand<DarwinResponse<CreatedAgeRateResponse>>;

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
         
            AgeRate ageRate = request.Model.Adapt<AgeRate>();
            await _repository.CreateAsync(ageRate);
            await _unitOfWork.CommitAsync();
            return DarwinResponse<CreatedAgeRateResponse>.Success(ageRate.Adapt<CreatedAgeRateResponse>(), 201);
        }
    }
    public class CreateAgeRateCommandValidator : AbstractValidator<Command>
    {
        public CreateAgeRateCommandValidator()
        {
            RuleFor(x => x.Model.Rate)
                .NotNull()
                .GreaterThanOrEqualTo(7);
            RuleFor(x => x.Model.Name)
                .NotNull()
                .NotEmpty()
                .Length(3, 64);
        }
    }
}
