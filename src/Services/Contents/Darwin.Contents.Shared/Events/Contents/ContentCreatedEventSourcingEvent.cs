using Darwin.Contents.Core.Entities;

namespace Darwin.Contents.Shared.Events.Contents;

public class ContentCreatedEventSourcingEvent : IEvent 
{
    public ContentCreatedEventSourcingEvent()
    {
        Moods= new ();
        Categories = new();
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? ImageUrl { get; set; }
    public string Lyrics { get; set; }
    public List<Category> Categories { get; set; }
    public List<Mood> Moods { get; set; }
} 