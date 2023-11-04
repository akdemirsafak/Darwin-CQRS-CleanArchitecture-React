using Darwin.Core.BaseDto;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Model.Request.PlayLists;
using Darwin.Model.Response.PlayLists;
using Darwin.Service.Common;
using Mapster;

namespace Darwin.Service.Features.PlayLists.Commands;

public static class UpdatePlayList
{
    public record Command(Guid id, UpdatePlayListRequest Model, string creatorId) : ICommand<DarwinResponse<UpdatedPlayListResponse>>;

    public class CommandHandler : ICommandHandler<Command, DarwinResponse<UpdatedPlayListResponse>>
    {
        private readonly IPlayListRepository _playListRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CommandHandler(IPlayListRepository playListRepository, IUnitOfWork unitOfWork)
        {
            _playListRepository = playListRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DarwinResponse<UpdatedPlayListResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var hasList= await _playListRepository.GetAsync(x=>x.Id==request.id && x.Creator.Id==request.creatorId);
            if (hasList is null)
            {
                return DarwinResponse<UpdatedPlayListResponse>.Fail("Liste bulunamadı.", 404);
            }
            if (!hasList.IsFavorite)
            {
                hasList.IsUsable = request.Model.IsUsable;
            }
            hasList.Name = request.Model.Name;
            hasList.Description = request.Model.Description;
            hasList.IsPublic = request.Model.IsPublic;
            hasList.UpdatedAt = DateTime.UtcNow.Ticks;

            if (!request.Model.IsUsable)
                hasList.DeletedAt = DateTime.UtcNow.Ticks;


            await _playListRepository.UpdateAsync(hasList);
            await _unitOfWork.CommitAsync();
            return DarwinResponse<UpdatedPlayListResponse>.Success(hasList.Adapt<UpdatedPlayListResponse>());
        }
    }
}
