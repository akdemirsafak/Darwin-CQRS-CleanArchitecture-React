using System.ComponentModel.DataAnnotations;

namespace Darwin.Core.Entities;

public class Music:BaseEntity
{
    [Required, MinLength(3), MaxLength(64)]
    public string Name { get; set; }
    public string Url { get; set; }
    [Required]
    public string Publishers { get; set; }
    public bool IsUsable { get; set; }
}
