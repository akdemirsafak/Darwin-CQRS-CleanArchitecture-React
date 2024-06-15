namespace Darwin.Contents.Shared.Events.Categories;

public record CategoryDeletedEventSourcingEvent(Guid id, bool isDeleted) : IEvent;