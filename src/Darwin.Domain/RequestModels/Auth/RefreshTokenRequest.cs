namespace Darwin.Domain.RequestModels.Authentications;

public record RefreshTokenRequest(string AccessToken, string RefreshToken);

