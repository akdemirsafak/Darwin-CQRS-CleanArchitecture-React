namespace Darwin.Model.Request.Users;

public record CreateUserRequest(string UserName, string Email, string Password);
