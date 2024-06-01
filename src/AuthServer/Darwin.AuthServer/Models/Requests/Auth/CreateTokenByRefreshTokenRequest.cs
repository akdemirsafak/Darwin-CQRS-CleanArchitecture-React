namespace Darwin.AuthServer.Models.Requests.Auth;
public record CreateTokenByRefreshTokenRequest(string AccessToken, string RefreshToken);