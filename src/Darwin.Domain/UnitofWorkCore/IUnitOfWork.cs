namespace Darwin.Domain.UnitofWorkCore;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken);
    int Commit();
}
