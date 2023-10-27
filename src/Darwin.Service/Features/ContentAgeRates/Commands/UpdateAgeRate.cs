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

public static class UpdateAgeRate
{
    public record Command(Guid AgeRateId, UpdateAgeRateRequest Model) : ICommand<DarwinResponse<UpdatedAgeRateResponse>>;

    public class CommandHandler : ICommandHandler<Command, DarwinResponse<UpdatedAgeRateResponse>>
    {
        private readonly IGenericRepository<AgeRate> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CommandHandler(IGenericRepository<AgeRate> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DarwinResponse<UpdatedAgeRateResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            AgeRate ageRate = await _repository.GetAsync(x => x.Id == request.AgeRateId);
            if (ageRate == null)
            {
                return DarwinResponse<UpdatedAgeRateResponse>.Fail("Age Rate Not Found.");
            }
            await _repository.UpdateAsync(ageRate);
            await _unitOfWork.CommitAsync();
            return DarwinResponse<UpdatedAgeRateResponse>.Success(ageRate.Adapt<UpdatedAgeRateResponse>());
        }
    }
    public class UpdateAgeRateCommandValidator : AbstractValidator<Command>
    {
        public UpdateAgeRateCommandValidator()
        {
            RuleFor(x => x.Model.Rate)
           .NotNull()
           .GreaterThanOrEqualTo(7);
            RuleFor(x => x.Model.Name)
                .NotNull()
                .Length(5, 30);
        }
    }
}
