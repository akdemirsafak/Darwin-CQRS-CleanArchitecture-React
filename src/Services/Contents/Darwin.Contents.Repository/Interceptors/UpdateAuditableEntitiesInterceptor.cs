using Darwin.Shared.Abstractions;
using Darwin.Shared.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Darwin.Contents.Repository.Interceptors;

public sealed class UpdateAuditableEntitiesInterceptor : SaveChangesInterceptor
{

    private readonly ICurrentUser _currentUser;

    public UpdateAuditableEntitiesInterceptor(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        DbContext? dbContext = eventData.Context;
        if (dbContext == null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        IEnumerable<EntityEntry<IAuditableEntity>> entries =
            dbContext
            .ChangeTracker
                .Entries<IAuditableEntity>();
        foreach (EntityEntry<IAuditableEntity> entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(x => x.CreatedAt).CurrentValue = DateTime.UtcNow;
                entry.Property(x => x.CreatedBy).CurrentValue = _currentUser.GetUserId;

            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property(x => x.UpdatedAt).CurrentValue = DateTime.UtcNow;
                entry.Property(x => x.UpdatedBy).CurrentValue = _currentUser.GetUserId;
            }
            if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;
                entry.Property(x => x.DeletedAt).CurrentValue = DateTime.UtcNow;
                entry.Entity.IsDeleted = true;
                entry.Property(x => x.DeletedBy).CurrentValue = _currentUser.GetUserId;
            }

        }
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

}
