using AutoMapper;
using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Common;
using Darwin.Model.Request.Moods;
using Darwin.Model.Response.Moods;
using Darwin.Service.Common;

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
        private readonly IGenericRepositoryAsync<Mood> _repository;
        private readonly IMapper _mapper;

        public Handler(IGenericRepositoryAsync<Mood> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DarwinResponse<CreatedMoodResponse>> Handle(CreateMoodCommand request, CancellationToken cancellationToken)
        {
            var existMood = await _repository.GetAsync(x => x.Name.ToLower().Trim() == request.Model.Name.ToLower().Trim());
            if (existMood is not null)
            {
                return DarwinResponse<CreatedMoodResponse>.Fail("Allready exist.");
            }
            Mood mood = _mapper.Map<Mood>(request.Model);
            await _repository.CreateAsync(mood);
            return DarwinResponse<CreatedMoodResponse>.Success(_mapper.Map<CreatedMoodResponse>(mood),201);
        }
    }
}
