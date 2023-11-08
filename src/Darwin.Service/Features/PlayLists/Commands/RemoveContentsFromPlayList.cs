using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Model.Request.PlayLists;
using Darwin.Model.Response.PlayLists;
using Darwin.Service.Common;
using Mapster;

namespace Darwin.Service.Features.PlayLists.Commands
{
    public static class RemoveContentsFromPlayList
    {
        public record Command(RemoveContentsFromPlayListRequest Model, string _currentUserId) : ICommand<DarwinResponse<GetPlayListByIdResponse>>;
        public class CommandHandler : ICommandHandler<Command, DarwinResponse<GetPlayListByIdResponse>>
        {
            private readonly IGenericRepository < PlayList > _playListRepository;
            private readonly IGenericRepository<Content> _contentRepository;
            private readonly IUnitOfWork _unitOfWork;

            public CommandHandler(IGenericRepository<PlayList> playListRepository, IUnitOfWork unitOfWork, IGenericRepository<Content> contentRepository)
            {
                _playListRepository = playListRepository;
                _unitOfWork = unitOfWork;
                _contentRepository = contentRepository;
            }

            public async Task<DarwinResponse<GetPlayListByIdResponse>> Handle(Command request, CancellationToken cancellationToken)
            {
                var existPlayList = await _playListRepository.GetAsync(x=>x.Id==request.Model.playListId);
                if (existPlayList is null)
                    return DarwinResponse<GetPlayListByIdResponse>.Fail("PlayList bulunamadı.", 404);

                foreach (var requestContentId in request.Model.contentIds)
                {
                    var content = await _contentRepository.GetAsync(x => x.Id == requestContentId);
                    existPlayList.Contents.Remove(content);
                }
                await _unitOfWork.CommitAsync();
                return DarwinResponse<GetPlayListByIdResponse>.Success(existPlayList.Adapt<GetPlayListByIdResponse>(), 200);
            }
        }
    }
}