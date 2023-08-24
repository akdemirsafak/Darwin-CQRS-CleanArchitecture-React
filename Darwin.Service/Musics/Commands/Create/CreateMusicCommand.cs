using AutoMapper;
using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Common;
using Darwin.Model.Request.Musics;
using Darwin.Service.Common;
using MediatR;

namespace Darwin.Service.Musics.Commands.Create;

public class CreateMusicCommand : ICommand<DarwinResponse<NoContent>>
{
    public CreateMusicRequest Model { get; }

    public CreateMusicCommand(CreateMusicRequest model)
    {
        Model = model;
    }
    public class Handler : ICommandHandler<CreateMusicCommand, DarwinResponse<NoContent>>
    {
        private readonly IGenericRepositoryAsync<Music> _repository;
        private readonly IMapper _mapper;

        public Handler(IGenericRepositoryAsync<Music> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DarwinResponse<NoContent>> Handle(CreateMusicCommand request, CancellationToken cancellationToken)
        {
            var entity= _mapper.Map<Music>(request.Model);
            await _repository.CreateAsync(entity);
            return DarwinResponse<NoContent>.Success(201);
        }
    }
}
