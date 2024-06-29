namespace Darwin.AuthServer.Models.Requests.Auth;

public record RegisterRequest (string Email, string Password, string Name, string LastName);