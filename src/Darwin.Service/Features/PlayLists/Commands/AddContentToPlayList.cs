using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Model.Request.PlayLists;
using Darwin.Model.Response.PlayLists;
using Darwin.Service.Common;
using Mapster;

namespace Darwin.Service.Features.PlayLists.Commands;

public static class AddContentToPlayList
{
    public record Command(AddContentToPlayListRequest Model) : ICommand<DarwinResponse<GetPlayListByIdResponse>>;
    public class CommandHandler : ICommandHandler<Command, DarwinResponse<GetPlayListByIdResponse>>
    {
        private readonly IPlayListRepository _playListRepository;
        private readonly IGenericRepository<Content> _contentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CommandHandler(IPlayListRepository playListRepository, IGenericRepository<Content> contentRepository, IUnitOfWork unitOfWork)
        {
            _playListRepository = playListRepository;
            _contentRepository = contentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DarwinResponse<GetPlayListByIdResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            PlayList hasPlayList = await _playListRepository.GetAsync(x => x.Id == request.Model.playListId);
            if (hasPlayList is null)
                return DarwinResponse<GetPlayListByIdResponse>.Fail("PlayList bulunamadı.", 400);


            Content hasMusic = await _contentRepository.GetAsync(x => x.Id == request.Model.contentId);
            if (hasMusic is null)
                return DarwinResponse<GetPlayListByIdResponse>.Fail("İçerik bulunamadı.", 400);


            hasPlayList.Contents.Add(hasMusic);
            await _playListRepository.UpdateAsync(hasPlayList);
            await _unitOfWork.CommitAsync();

            return DarwinResponse<GetPlayListByIdResponse>.Success(hasPlayList.Adapt<GetPlayListByIdResponse>(), 201);

        }
    }
}
