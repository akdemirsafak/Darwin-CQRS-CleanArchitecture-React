using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Model.Response.PlayLists;
using Darwin.Service.Common;

namespace Darwin.Service.Features.PlayLists.Commands;

public static class DeletePlayList
{
    public record Command(Guid id) : ICommand<DarwinResponse<DeletedPlayListResponse>>;

    public class CommandHandler(IGenericRepository<PlayList> _playListRepository) 
        : ICommandHandler<Command, DarwinResponse<DeletedPlayListResponse>>
    {

        public async Task<DarwinResponse<DeletedPlayListResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var hasPlayList= await _playListRepository.GetAsync(x=>x.Id==request.id);

            if (hasPlayList is null)
                return DarwinResponse<DeletedPlayListResponse>.Fail("Liste bulunamadı.Daha önce silinmiş olabilir.", 400);

            if (hasPlayList.IsFavorite)
                return DarwinResponse<DeletedPlayListResponse>.Fail("Favori içeriklerim listesi silinemez.", 400);

            hasPlayList.IsUsable = false;
            await _playListRepository.UpdateAsync(hasPlayList);

            return DarwinResponse<DeletedPlayListResponse>.Success(204);
        }
    }
}
