using Darwin.Contentlists.Core.Entities;
using Darwin.Contentlists.Core.Repositories;
using Darwin.Contentlists.Repository.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Darwin.Contentlists.Repository.Repositories;

public class PlaylistRepository : IPlaylistRepository
{
    private readonly AppDbContext _dbContext;

    public PlaylistRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAsync(Playlist playlist)
    {
        await _dbContext.Playlists.AddAsync(playlist);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Playlist playlist)
    {
        _dbContext.Playlists.Remove(playlist);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Playlist>> GetAllAsync()
    {
        return await _dbContext.Playlists.ToListAsync();
    }

    public async Task<Playlist> GetByIdAsync(Guid id)
    {
        return await _dbContext.Playlists.FindAsync(id);
    }

    public async Task Update(Playlist playlist)
    {
        _dbContext.Playlists.Update(playlist);
        await _dbContext.SaveChangesAsync();
    }
}
