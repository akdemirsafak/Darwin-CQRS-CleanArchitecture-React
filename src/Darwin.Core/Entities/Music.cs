using System.ComponentModel.DataAnnotations;

namespace Darwin.Core.Entities;

public class Music : BaseEntity
{
    public Music()
    {
        Categories=new HashSet<Category>();
        Moods = new HashSet<Mood>();
        AgeRate = new();
    }
    [Required, MinLength(3), MaxLength(64)]
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string? Lyrics { get; set; }
    public bool IsUsable { get; set; }
    public virtual ICollection<Category> Categories { get; set; }
    public virtual ICollection<Mood> Moods { get; set; }
    public virtual AgeRate AgeRate { get; set; }
}
