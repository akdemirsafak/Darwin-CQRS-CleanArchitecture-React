using System.ComponentModel.DataAnnotations;

namespace Darwin.Core.Entities;

public class Music : BaseEntity
{
    public Music()
    {
        //HashSet<> List'e göre daha az kaynak harcar ve unique bir yapılanmadır.
        Categories = new HashSet<Category>();
        AgeRate = new();
        Moods = new HashSet<Mood>();
        AgeRate = new();
    }
    [Required, MinLength(3), MaxLength(64)]
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string? Lyrics { get; set; }
    public bool IsUsable { get; set; }
    public virtual AgeRate AgeRate { get; set; }
    public virtual ICollection<Category> Categories { get; set; }
    public virtual ICollection<Mood> Moods { get; set; }
    public virtual ICollection<PlayList> PlayLists { get; set; }

}
