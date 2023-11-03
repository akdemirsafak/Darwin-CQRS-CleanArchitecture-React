namespace Darwin.Service.Notifications.UserCreated;

public record UserCreatedMailModel(string To,string confirmationAddress,string userName,DateTime createdAt);
