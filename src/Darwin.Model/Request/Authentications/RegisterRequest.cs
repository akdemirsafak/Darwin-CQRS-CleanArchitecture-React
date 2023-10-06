namespace Darwin.Model.Request.Authentications;

public record RegisterRequest(string UserName, string Email, string Password);