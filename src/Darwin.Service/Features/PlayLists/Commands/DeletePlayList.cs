using Darwin.Core.BaseDto;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Model.Response.PlayLists;
using Darwin.Service.Common;

namespace Darwin.Service.Features.PlayLists.Commands;

public static class DeletePlayList
{
    public record Command(Guid id) : ICommand<DarwinResponse<DeletedPlayListResponse>>;

    public class CommandHandler : ICommandHandler<Command, DarwinResponse<DeletedPlayListResponse>>
    {
        private readonly IPlayListRepository _playListRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CommandHandler(IPlayListRepository playListRepository, IUnitOfWork unitOfWork)
        {
            _playListRepository = playListRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DarwinResponse<DeletedPlayListResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var hasPlayList= await _playListRepository.GetAsync(x=>x.Id==request.id);
            if (hasPlayList is null)
            {
                return DarwinResponse<DeletedPlayListResponse>.Fail("Liste bulunamadı.Daha önce silinmiş olabilir.", 400);
            }
            hasPlayList.IsUsable = false;
            hasPlayList.DeletedAt = DateTime.UtcNow.Ticks;
            await _playListRepository.UpdateAsync(hasPlayList);
            await _unitOfWork.CommitAsync();
            return DarwinResponse<DeletedPlayListResponse>.Success(204);
        }
    }
}
