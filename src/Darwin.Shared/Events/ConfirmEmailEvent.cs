namespace Darwin.Shared.Events;

public class ConfirmEmailEvent
{
    public string UserName { get; set; }
    public string FullName { get; set; }
    public string To { get; set; }
    public string ConfirmationUrl { get; set; }
}
