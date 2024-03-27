namespace Darwin.Domain.Entities;

public interface IAuditableEntity
{
    DateTime CreatedOnUtc { get; set; }
    string? CreatedBy { get; set; }
    DateTime? UpdatedOnUtc { get; set; }
    string? UpdatedBy { get; set; }


    bool IsDeleted { get; set; }
    DateTime? DeletedOnUtc { get; set; }
    string? DeletedBy { get; set; }
}
