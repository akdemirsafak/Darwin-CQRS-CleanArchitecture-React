using Darwin.Contentlists.Core.Entities;

namespace Darwin.Contentlists.Core.Repositories;

public interface IPlaylistRepository
{
    Task CreateAsync(Playlist playlist);
    Task<Playlist> GetByIdAsync(Guid id);
    Task<List<Playlist>> GetAllAsync();
    Task Update(Playlist playlist);
    Task Delete(Playlist playlist);
}
