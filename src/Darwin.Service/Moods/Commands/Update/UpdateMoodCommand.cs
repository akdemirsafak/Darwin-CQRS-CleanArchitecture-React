using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Model.Request.Moods;
using Darwin.Model.Response.Moods;
using Darwin.Service.Common;
using Mapster;

namespace Darwin.Service.Moods.Commands.Update;

public class UpdateMoodCommand : ICommand<DarwinResponse<UpdatedMoodResponse>>
{
    public Guid Id { get; }
    public UpdateMoodRequest Model { get; }
    public UpdateMoodCommand(Guid id, UpdateMoodRequest model)
    {
        Id = id;
        Model = model;
    }

    public class Handler : ICommandHandler<UpdateMoodCommand, DarwinResponse<UpdatedMoodResponse>>
    {
        private readonly IGenericRepository<Mood> _repository;
        private readonly IUnitOfWork _unitOfWork;


        public Handler(IGenericRepository<Mood> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DarwinResponse<UpdatedMoodResponse>> Handle(UpdateMoodCommand request, CancellationToken cancellationToken)
        {
            var existMood= await _repository.GetAsync(x=>x.Id==request.Id);
            if (existMood == null)
            {
                return DarwinResponse<UpdatedMoodResponse>.Fail("");
            }
            existMood.ImageUrl = request.Model.ImageUrl;
            existMood.Name = request.Model.Name;
            existMood.IsUsable = request.Model.IsUsable;
            existMood.UpdatedAt = DateTime.UtcNow.Ticks;
            await _repository.UpdateAsync(existMood);
            await _unitOfWork.CommitAsync();
            return DarwinResponse<UpdatedMoodResponse>.Success(existMood.Adapt<UpdatedMoodResponse>(), 204);
        }
    }
}

