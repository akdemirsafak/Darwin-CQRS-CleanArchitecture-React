namespace Darwin.Model.Request.Users;

public record RegisterRequest(string Email, string Password, string ConfirmPassword);