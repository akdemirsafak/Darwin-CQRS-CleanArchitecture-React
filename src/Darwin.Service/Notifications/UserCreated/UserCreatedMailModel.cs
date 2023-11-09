namespace Darwin.Service.Notifications.UserCreated;

public record UserCreatedMailModel(string To, string confirmationAddress, DateTime createdAt);
