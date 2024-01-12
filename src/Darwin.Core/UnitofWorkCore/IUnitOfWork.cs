namespace Darwin.Core.UnitofWorkCore;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken);
    int Commit();
}
