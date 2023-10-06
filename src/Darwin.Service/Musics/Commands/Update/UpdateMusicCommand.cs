using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Request.Musics;
using Darwin.Model.Response.Musics;
using Darwin.Service.Common;
using Mapster;

namespace Darwin.Service.Musics.Commands.Update;

public class UpdateMusicCommand : ICommand<DarwinResponse<UpdatedMusicResponse>>
{
    public Guid Id { get; }
    public UpdateMusicRequest Model { get; }
    public UpdateMusicCommand(Guid id, UpdateMusicRequest model)
    {
        Id = id;
        Model = model;
    }

    public class Handler : ICommandHandler<UpdateMusicCommand, DarwinResponse<UpdatedMusicResponse>>
    {
        private readonly IGenericRepository<Music> _repository;

        public Handler(IGenericRepository<Music> repository)
        {
            _repository = repository;
        }

        public async Task<DarwinResponse<UpdatedMusicResponse>> Handle(UpdateMusicCommand request, CancellationToken cancellationToken)
        {
            var existMusic = await _repository.GetAsync(x => x.Id == request.Id);
            if (existMusic == null)
            {
                return DarwinResponse<UpdatedMusicResponse>.Fail("");
            }
            existMusic.ImageUrl = request.Model.ImageUrl;
            existMusic.Name = request.Model.Name != existMusic.Name ? request.Model.Name : existMusic.Name;
            existMusic.IsUsable = request.Model.IsUsable;
            existMusic.UpdatedAt = DateTime.UtcNow.Ticks;
            await _repository.UpdateAsync(existMusic);
            return DarwinResponse<UpdatedMusicResponse>.Success(existMusic.Adapt<UpdatedMusicResponse>(), 204);
        }
    }
}
