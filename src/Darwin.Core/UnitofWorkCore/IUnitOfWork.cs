namespace Darwin.Core.UnitofWorkCore;

public interface IUnitOfWork
{
    Task CommitAsync();
    int Commit();
}
