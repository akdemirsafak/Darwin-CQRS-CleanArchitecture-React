using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Model.Request.Moods;
using Darwin.Model.Response.Moods;
using Darwin.Service.Common;
using Mapster;

namespace Darwin.Service.Moods.Commands.Create;

public class CreateMoodCommand : ICommand<DarwinResponse<CreatedMoodResponse>>
{
    public CreateMoodRequest Model { get; }

    public CreateMoodCommand(CreateMoodRequest model)
    {
        Model = model;
    }
    public class Handler : ICommandHandler<CreateMoodCommand, DarwinResponse<CreatedMoodResponse>>
    {
        private readonly IGenericRepository<Mood> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IGenericRepository<Mood> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DarwinResponse<CreatedMoodResponse>> Handle(CreateMoodCommand request, CancellationToken cancellationToken)
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
}
