namespace Darwin.Core.Entities;

public interface IAuditableEntity
{
    DateTime CreatedOnUtc { get; set; }
    DateTime? UpdatedOnUtc { get; set; }
}
