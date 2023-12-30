using Darwin.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Darwin.Service.TokenOperations;

public class TokenService : ITokenService
{
    private readonly AppTokenOptions _tokenOptions;
    private readonly UserManager<AppUser> _userManager;

    public TokenService(IOptions<AppTokenOptions> tokenOptions, UserManager<AppUser> userManager)
    {
        _tokenOptions = tokenOptions.Value;
        _userManager = userManager;
    }
    public async Task<TokenResponse> CreateTokenAsync(AppUser appUser)
    {
        var accessTokenExpiration = DateTime.UtcNow.AddMinutes(_tokenOptions.AccessTokenExpiration);
        var refreshTokenExpiration = DateTime.UtcNow.AddMinutes(_tokenOptions.RefreshTokenExpiration);

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var claims=GetClaims(appUser,_tokenOptions.Audience);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer:_tokenOptions.Issuer,
            expires: accessTokenExpiration,
            notBefore: DateTime.UtcNow,
            claims: claims,
            signingCredentials: signingCredentials);

        var handler = new JwtSecurityTokenHandler();
        var token = handler.WriteToken(jwtSecurityToken);

        var tokenDto = new TokenResponse
        {
            AccessToken = token,
            RefreshToken = CreateRefreshToken(),
            AccessTokenExpiration = accessTokenExpiration,
            RefreshTokenExpiration = refreshTokenExpiration
        };

        await _userManager.AddClaimsAsync(appUser,claims);

        return tokenDto;
    }
    public string CreateRefreshToken()
    {
        var numberByte = new byte[32];
        using var rnd = RandomNumberGenerator.Create();
        rnd.GetBytes(numberByte);

        return Convert.ToBase64String(numberByte);
    }
    private IEnumerable<Claim> GetClaims(AppUser appUser, List<String> audiences)
    {
        var roles= _userManager.GetRolesAsync(appUser).Result;
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, appUser.Id),
            new(JwtRegisteredClaimNames.Email, appUser.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) //Jwt'nin id sini temsil eder.

        };
        claims.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
        claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));

        return claims;
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string? token)
    {
        TokenValidationParameters tokenValidationParameters= new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = _tokenOptions!.Issuer,
            ValidAudiences = _tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey))
        };

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        var principal=tokenHandler.ValidateToken(token,tokenValidationParameters,out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken
            || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature,
            StringComparison.InvariantCultureIgnoreCase))

                throw new SecurityTokenException();

        return principal;
    }
}
