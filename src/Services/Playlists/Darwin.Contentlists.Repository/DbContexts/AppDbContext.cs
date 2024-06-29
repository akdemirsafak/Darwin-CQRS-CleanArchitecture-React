using Darwin.Contentlists.Core.Entities;
using Darwin.Shared.Auth;
using Microsoft.EntityFrameworkCore;

namespace Darwin.Contentlists.Repository.DbContexts;

public class AppDbContext : DbContext
{
    private readonly ICurrentUser _currentUser;

    public AppDbContext(DbContextOptions<AppDbContext> options,  
        ICurrentUser currentUser) : base(options)
    {
        _currentUser = currentUser;
    }


    public DbSet<Playlist> Playlists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Playlist>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        AddAuditInfo();
        return base.SaveChangesAsync(cancellationToken);
    }
    private void AddAuditInfo()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is Playlist && (e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted));

        foreach (var entry in entries)
        {
            var entity = (Playlist)entry.Entity;
            var now = DateTime.Now;
            var userId = _currentUser.GetUserId;
            var username = _currentUser.GetUserName;
            if (entry.State == EntityState.Added)
            {
                entity.CreatedAt = now;
                entity.CreatedBy = userId;
                entity.CreatorName = username;
            }
            else if (entry.State == EntityState.Modified)
            {
                entity.UpdatedAt = now;
                entity.UpdatedBy = userId;
            }
            else if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;  // Soft delete
                entity.DeletedAt = now;
                entity.DeletedBy = userId;
                entity.IsDeleted = true;
            }
        }
    }
}
