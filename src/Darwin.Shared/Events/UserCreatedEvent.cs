namespace Darwin.Shared.Events;

public class UserCreatedEvent
{
    public string UserName { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime CreatedDate { get; set; }
    public string EmailConfirmationLink { get; set; }
}

