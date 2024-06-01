namespace Darwin.AuthServer.Requests.Users;
public record ResetPasswordRequest(string Password, string PasswordConfirm);
