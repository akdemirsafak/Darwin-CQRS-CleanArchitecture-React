namespace Darwin.Domain.RequestModels.Users;

public record RegisterRequest(string Email, string Password, string ConfirmPassword);