namespace Darwin.Contents.Shared.Events.Categories;

public class CategoryUpdatedEventSourcingEvent : IEvent
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? ImageUrl { get; set; }
}