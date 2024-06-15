using Darwin.Shared.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Darwin.Contents.Core.Entities;

public class Mood : BaseEntity, IAuditableEntity
{
    public Mood()
    {
        Contents = new HashSet<Content>();
    }
    [Required]
    [MaxLength(32)]
    public string Name { get; set; }
    public string? ImageUrl { get; set; }
    public virtual ICollection<Content> Contents { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}
