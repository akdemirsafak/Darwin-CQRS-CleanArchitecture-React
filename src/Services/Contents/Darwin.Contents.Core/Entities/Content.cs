using Darwin.Shared.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Darwin.Contents.Core.Entities;

public class Content : BaseEntity, IAuditableEntity
{
    public Content()
    {
        //HashSet<> List'e göre daha az kaynak harcar ve unique bir yapılanmadır.
        Categories = new HashSet<Category>();
        Moods = new HashSet<Mood>();
    }
    [Required]
    [MaxLength(32)]
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    [MaxLength(512)]
    public string? Lyrics { get; set; }
    public virtual ICollection<Category> Categories { get; set; }
    public virtual ICollection<Mood> Moods { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}
