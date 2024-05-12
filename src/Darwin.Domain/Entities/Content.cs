using System.ComponentModel.DataAnnotations;

namespace Darwin.Domain.Entities;

public class Content : BaseEntity, IAuditableEntity
{
    public Content()
    {
        //HashSet<> List'e göre daha az kaynak harcar ve unique bir yapılanmadır.
        Categories = new HashSet<Category>();
        Moods = new HashSet<Mood>();
    }
    [Required, MinLength(3), MaxLength(64)]
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string? Lyrics { get; set; }
    public bool IsUsable { get; set; }
    public virtual ICollection<Category> Categories { get; set; }
    public virtual ICollection<Mood> Moods { get; set; }
    public virtual ICollection<PlayList> PlayLists { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? UpdatedOnUtc { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedOnUtc { get; set; }
    public string? DeletedBy { get ; set; }
}
