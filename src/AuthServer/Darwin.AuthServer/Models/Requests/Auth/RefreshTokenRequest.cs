namespace Darwin.AuthServer.Requests.Auth;

public record RefreshTokenRequest(string AccessToken, string RefreshToken);

