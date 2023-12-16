namespace Darwin.Service.Events.UserCreated;

public record UserCreatedMailModel(string To, string confirmationAddress, DateTime createdAt);
