using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using MediatR;

namespace Darwin.Service.Notifications.UserCreated;

public record UserCreatedCreateFavoritePlaylistEvent(string userId) : INotification;

public class UserCreatedCreateFavoritePlaylistEventHandler : INotificationHandler<UserCreatedCreateFavoritePlaylistEvent>
{
    private readonly IPlayListRepository _playListRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserCreatedCreateFavoritePlaylistEventHandler(IPlayListRepository playListRepository, IUnitOfWork unitOfWork)
    {
        _playListRepository = playListRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UserCreatedCreateFavoritePlaylistEvent notification, CancellationToken cancellationToken)
    {
        var entity=new PlayList
        {
            Name="Favorites",
            CreatorId=notification.userId,
            IsPublic=false,
            IsUsable=true,
            IsFavorite=true,
            Description="Your favorite contents here."
        };
        await _playListRepository.CreateAsync(entity);
        await _unitOfWork.CommitAsync();
    }
}