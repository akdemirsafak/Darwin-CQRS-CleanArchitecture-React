using System.ComponentModel.DataAnnotations;

namespace Darwin.Core.Entities;

public class Category : BaseEntity, IAuditableEntity
{
    public Category()
    {
        Contents = new HashSet<Content>();
    }

    [Required, MinLength(3), MaxLength(64)]
    public string Name { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsUsable { get; set; }
    public virtual ICollection<Content> Contents { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? UpdatedOnUtc { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}