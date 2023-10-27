using System.ComponentModel.DataAnnotations;

namespace Darwin.Core.Entities;

public class PlayList : BaseEntity
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
    public virtual ICollection<Content> Contents { get; set; }
}
