namespace Darwin.Contents.Shared.Events.Moods;

public class MoodUpdatedEventSourcingEvent : IEvent 
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? ImageUrl { get; set; }
}