namespace Darwin.Shared.Events;

public class SendNewContentsEvent
{
    public List<string> To { get; set; }
    public string Subject { get; set; } // Title
    public string Body { get; set; }

}
