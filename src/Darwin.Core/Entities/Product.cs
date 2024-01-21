namespace Darwin.Core.Entities;

public class Product : BaseEntity, IAuditableEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedOnUtc { get; set; }
    public string? UpdatedBy { get; set; }
}
