namespace Darwin.Application.Events.UserCreated;

public record UserCreatedMailModel(string To, string confirmationAddress, DateTime createdAt);
