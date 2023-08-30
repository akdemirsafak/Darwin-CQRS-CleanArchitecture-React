namespace Darwin.Core.Entities;

public class MusicMood:BaseEntity
{
    public Guid MusicId { get; set; }
    public Music Music { get; set; }
    public Guid MoodId { get; set; }
    public Mood Mood { get; set; }

}
