namespace Darwin.Service.TokenOperations;

public class AppTokenOptions
{
    public List<string> Audiences { get; set; }
    public string Issuer { get; set; }
    public int AccessTokenExpiration { get; set; }
    public int RefreshTokenExpiration { get; set; }
    public string SecurityKey { get; set; }
}
