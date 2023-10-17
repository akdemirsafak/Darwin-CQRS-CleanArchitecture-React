using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Model.Response.AgeRates;
using Darwin.Service.Common;
using Mapster;

namespace Darwin.Service.Features.ContentAgeRates.Commands;

public static class DeleteAgeRate
{
    public record Command(Guid AgeRateId) : ICommand<DarwinResponse<DeletedAgeRateResponse>>;

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
            AgeRate ageRate= await _repository.GetAsync(x=>x.Id==request.AgeRateId);
            if (ageRate==null)
            {
                return DarwinResponse<DeletedAgeRateResponse>.Fail("Age Rate Not Found.");
            }
            await _repository.RemoveAsync(ageRate);
            await _unitOfWork.CommitAsync();
            return DarwinResponse<DeletedAgeRateResponse>.Success(ageRate.Adapt<DeletedAgeRateResponse>(), 204);
        }
    }
}
