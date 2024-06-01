namespace Darwin.AuthServer.Models.Requests.Users;

public record ChangePasswordRequest(string Email, string CurrentPassword, string NewPassword);
