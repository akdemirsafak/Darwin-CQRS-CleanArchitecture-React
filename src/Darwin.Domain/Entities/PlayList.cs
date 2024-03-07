using System.ComponentModel.DataAnnotations;

namespace Darwin.Domain.Entities;

public class PlayList : BaseEntity, IAuditableEntity
{
    public PlayList()
    {
        Contents = new HashSet<Content>();
    }
    [Required]
    [MinLength(3), MaxLength(64)]
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsPublic { get; set; }
    public bool IsUsable { get; set; }
    public bool IsFavorite { get; set; } = false;
    public virtual ICollection<Content> Contents { get; set; }
    public virtual AppUser Creator { get; set; }
    public virtual string CreatorId { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? UpdatedOnUtc { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}
