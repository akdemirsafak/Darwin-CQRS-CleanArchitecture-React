using Darwin.Shared.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Security.Claims;

namespace Darwin.AuthServer.Interceptors;

public sealed class AuditInterceptor : SaveChangesInterceptor
{

    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuditInterceptor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
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
                entry.Property(x => x.CreatedOnUtc).CurrentValue = DateTime.UtcNow;
                entry.Property(x => x.CreatedBy).CurrentValue = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property(x => x.UpdatedOnUtc).CurrentValue = DateTime.UtcNow;
                entry.Property(x => x.UpdatedBy).CurrentValue = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }
            if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;
                entry.Property(x => x.DeletedOnUtc).CurrentValue = DateTime.UtcNow;
                entry.Entity.IsDeleted = true;
                entry.Property(x => x.DeletedBy).CurrentValue = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }

        }
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

}
