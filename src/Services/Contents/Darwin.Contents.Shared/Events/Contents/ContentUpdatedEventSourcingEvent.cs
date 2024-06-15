namespace Darwin.Contents.Shared.Events.Contents;

public class ContentUpdatedEventSourcingEvent : IEvent
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Lyrics { get; set; }
    public string? ImageUrl { get; set; }
}