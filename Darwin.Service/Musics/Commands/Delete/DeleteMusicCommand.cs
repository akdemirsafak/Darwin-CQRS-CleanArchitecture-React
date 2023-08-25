using AutoMapper;
using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Common;
using Darwin.Service.Common;

namespace Darwin.Service.Musics.Commands.Delete;

public class DeleteMusicCommand:ICommand<DarwinResponse<NoContent>>
{
    public Guid Id { get; }

    public DeleteMusicCommand(Guid id)
    {
        Id = id;
    }

    public class Handler : ICommandHandler<DeleteMusicCommand, DarwinResponse<NoContent>>
    {
        private readonly IGenericRepositoryAsync<Music> _repository;
        private readonly IMapper _mapper;

        public Handler(IGenericRepositoryAsync<Music> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DarwinResponse<NoContent>> Handle(DeleteMusicCommand request, CancellationToken cancellationToken)
        {
            var existMusic= await _repository.GetAsync(x=>x.Id==request.Id);
            if (existMusic == null)
                return DarwinResponse<NoContent>.Fail("");
            existMusic.IsUsable = false;
            existMusic.DeletedAt = DateTime.UtcNow.Ticks;
            await _repository.UpdateAsync(existMusic);
            return DarwinResponse<NoContent>.Success(204);
        }
    }
}
