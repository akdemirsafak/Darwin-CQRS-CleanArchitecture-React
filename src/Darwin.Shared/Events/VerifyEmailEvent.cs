namespace Darwin.Shared.Events;

public class VerifyEmailEvent
{
    public string UserName { get; set; }
    public string FullName { get; set; }
    public string To { get; set; }
    public string ConfirmationUrl { get; set; }
}
