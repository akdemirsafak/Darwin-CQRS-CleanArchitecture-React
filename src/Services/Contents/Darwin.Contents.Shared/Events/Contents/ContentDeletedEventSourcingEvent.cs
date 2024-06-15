namespace Darwin.Contents.Shared.Events.Contents;

public class ContentDeletedEventSourcingEvent:IEvent
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
} 