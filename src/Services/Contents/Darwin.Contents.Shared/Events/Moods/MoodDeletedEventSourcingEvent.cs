namespace Darwin.Contents.Shared.Events.Moods;

public record MoodDeletedEventSourcingEvent(Guid id, bool IsDeleted) : IEvent;
