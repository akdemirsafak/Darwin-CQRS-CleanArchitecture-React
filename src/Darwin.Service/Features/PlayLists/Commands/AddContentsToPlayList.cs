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
        private readonly IPlayListRepository _playListRepository;
        private readonly IContentRepository _contentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CommandHandler(IPlayListRepository playListRepository, IContentRepository contentRepository, IUnitOfWork unitOfWork)
        {
            _playListRepository = playListRepository;
            _contentRepository = contentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DarwinResponse<GetPlayListByIdResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            PlayList hasPlayList = await _playListRepository.GetPlayListByIdWithContentsAsync(request.Model.playListId);
            if (hasPlayList is null)
                return DarwinResponse<GetPlayListByIdResponse>.Fail("PlayList bulunamadı.", 400);

            foreach (var contentId in request.Model.contentIds)
            {
                var content = await _contentRepository.GetAsync(x => x.Id == contentId);
                if (content is null) return DarwinResponse<GetPlayListByIdResponse>.Fail("Bazı içerikler bulunamadı.", 400);
                else
                {
                    hasPlayList.Contents.Add(content);
                }
            }

            await _playListRepository.UpdateAsync(hasPlayList);
            await _unitOfWork.CommitAsync();

            return DarwinResponse<GetPlayListByIdResponse>.Success(hasPlayList.Adapt<GetPlayListByIdResponse>(), 201);

        }
    }
}
