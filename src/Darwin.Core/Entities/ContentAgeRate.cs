using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Darwin.Core.Entities;

public class ContentAgeRate
{
    public Guid Id { get; set; }
    [Required]
    public int Rate { get; set; }
    [Required,NotNull,MinLength(0),MaxLength(30)]
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public virtual IList<Music> Musics { get; set; }
}
