using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Model.Request.PlayLists;
using Darwin.Model.Response.PlayLists;
using Darwin.Service.Common;
using Mapster;

namespace Darwin.Service.Features.PlayLists.Commands;

public static class AddContentsToPlayList
{
    public record Command(AddContentsToPlayListRequest Model, string creatorId) : ICommand<DarwinResponse<GetPlayListByIdResponse>>;
    public class CommandHandler : ICommandHandler<Command, DarwinResponse<GetPlayListByIdResponse>>
    {
        private readonly IGenericRepository<PlayList> _playListRepository;
        private readonly IGenericRepository <Content> _contentRepository;

        public CommandHandler(IGenericRepository<PlayList> playListRepository, 
            IGenericRepository<Content> contentRepository)
        {
            _playListRepository = playListRepository;
            _contentRepository = contentRepository;
        }

        public async Task<DarwinResponse<GetPlayListByIdResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            PlayList hasPlayList = await _playListRepository.GetAsync(x=>x.Id == request.Model.playListId);
            if (hasPlayList is null)
                return DarwinResponse<GetPlayListByIdResponse>.Fail("PlayList bulunamadı.", 400);

            foreach (var contentId in request.Model.contentIds)
            {
                var content = await _contentRepository.GetAsync(x => x.Id == contentId);
                if (content is null)
                    return DarwinResponse<GetPlayListByIdResponse>.Fail("Bazı içerikler bulunamadı.", 400);
                else
                {
                    hasPlayList.Contents.Add(content);
                }
            }

            await _playListRepository.UpdateAsync(hasPlayList);

            return DarwinResponse<GetPlayListByIdResponse>.Success(hasPlayList.Adapt<GetPlayListByIdResponse>(), 201);
        }
    }
}
