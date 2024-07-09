using Darwin.Contentlists.Core.Entities;
using Darwin.Contentlists.Core.Repositories;
using Darwin.Contentlists.Repository.DbContexts;
using Darwin.Shared.Events;
using MassTransit;

namespace Darwin.Contentlists.Service.Consumers;

public class UserCreatedCreateFavoritePlaylistEventConsumer : IConsumer<UserCreatedCreateFavoritePlaylistEvent>
{
    private readonly AppDbContext _dbContext;

    public UserCreatedCreateFavoritePlaylistEventConsumer(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<UserCreatedCreateFavoritePlaylistEvent> context)
    {
        await _dbContext.Playlists.AddAsync(new Playlist
        {
            Name = "Favorites",
            ContentIds = null,
            CreatedBy = context.Message.UserId,
            CreatedAt = context.Message.CreatedDate,
            IsPublic = false,
            CreatorName = $"{context.Message.Name} {context.Message.LastName}",
            IsFavorite = true
        });
        _dbContext.SaveChanges();
        
    }
}
