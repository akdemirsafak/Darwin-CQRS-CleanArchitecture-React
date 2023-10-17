namespace Darwin.Core.Entities;

public class AgeRate:BaseEntity
{
    public int Rate { get; set; }
    public string Name { get; set; }
    public ICollection<Music> Musics { get; set; }
}
