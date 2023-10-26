using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Model.Request.PlayLists;
using Darwin.Model.Response.PlayLists;
using Darwin.Service.Common;
using Mapster;

namespace Darwin.Service.Features.PlayLists.Commands;

public static class AddMusicToPlayList
{
    public record Command(AddMusicToPlayListRequest Model) : ICommand<DarwinResponse<GetPlayListByIdResponse>>;
    public class CommandHandler : ICommandHandler<Command, DarwinResponse<GetPlayListByIdResponse>>
    {
        private readonly IPlayListRepository _playListRepository;
        private readonly IGenericRepository<Music> _musicRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CommandHandler(IPlayListRepository playListRepository, IGenericRepository<Music> musicRepository, IUnitOfWork unitOfWork)
        {
            _playListRepository = playListRepository;
            _musicRepository = musicRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DarwinResponse<GetPlayListByIdResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            PlayList hasPlayList = await _playListRepository.GetAsync(x => x.Id == request.Model.playListId);
            if (hasPlayList is null)
                return DarwinResponse<GetPlayListByIdResponse>.Fail("PlayList bulunamadı.", 400);


            Music hasMusic = await _musicRepository.GetAsync(x => x.Id == request.Model.musicId);
            if (hasMusic is null)
                return DarwinResponse<GetPlayListByIdResponse>.Fail("İçerik bulunamadı.", 400);


            hasPlayList.Musics.Add(hasMusic);
            await _playListRepository.UpdateAsync(hasPlayList);
            await _unitOfWork.CommitAsync();

            return DarwinResponse<GetPlayListByIdResponse>.Success(hasPlayList.Adapt<GetPlayListByIdResponse>(), 201);

        }
    }
}
