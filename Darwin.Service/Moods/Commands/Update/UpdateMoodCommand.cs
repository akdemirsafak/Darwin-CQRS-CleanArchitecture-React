using AutoMapper;
using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Common;
using Darwin.Model.Request.Categories;
using Darwin.Model.Request.Moods;
using Darwin.Model.Response.Moods;
using Darwin.Service.Common;

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
        private readonly IGenericRepositoryAsync<Mood> _repository;
        private readonly IMapper _mapper;

        public Handler(IGenericRepositoryAsync<Mood> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
            existMood.UpdatedAt = DateTime.UtcNow.Ticks;
            await _repository.UpdateAsync(existMood);
            return DarwinResponse<UpdatedMoodResponse>.Success(_mapper.Map<UpdatedMoodResponse>(existMood),204);
        }
    }
}

