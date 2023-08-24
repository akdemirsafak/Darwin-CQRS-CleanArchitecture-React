using AutoMapper;
using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Common;
using Darwin.Model.Request.Musics;
using Darwin.Service.Common;

namespace Darwin.Service.Musics.Commands.Update;

public class UpdateMusicCommand : ICommand<DarwinResponse<NoContent>>
{    
    public Guid Id { get; }
    public UpdateMusicRequest Model { get; }
    public UpdateMusicCommand(Guid id, UpdateMusicRequest model)
    {
        Id = id;
        Model = model;
    }
 
    public class Handler : ICommandHandler<UpdateMusicCommand, DarwinResponse<NoContent>>
    {
        private readonly IGenericRepositoryAsync<Music> _repository;
        private readonly IMapper _mapper;

        public Handler(IGenericRepositoryAsync<Music> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DarwinResponse<NoContent>> Handle(UpdateMusicCommand request, CancellationToken cancellationToken)
        {
            var existMusic= await _repository.GetAsync(x=>x.Id==request.Id);
            if (existMusic == null)
            {
                return DarwinResponse<NoContent>.Fail("");
            }
            existMusic.Url= request.Model.Url != existMusic.Url ? request.Model.Url : existMusic.Url;
            existMusic.Name= request.Model.Name != existMusic.Name ? request.Model.Name : existMusic.Name;
            existMusic.Publishers= request.Model.Publishers != existMusic.Publishers ? request.Model.Publishers : existMusic.Publishers;
            existMusic.IsUsable = request.Model.IsUsable;
            existMusic.UpdatedAt = DateTime.UtcNow.Ticks;
            await _repository.UpdateAsync(existMusic);
            return DarwinResponse<NoContent>.Success(204);
        }
    }
}
