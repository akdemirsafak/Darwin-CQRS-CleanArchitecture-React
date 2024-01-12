namespace Darwin.Model.Request.Authentications;

public record RefreshTokenRequest(string AccessToken, string RefreshToken);

