using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

public static class JwtTokenGenerator
{
    private static string _securityKey;
    private static int _expiryInMinutes;
    private static string _issuer;
    private static string _audience;

    private static bool _isInitialized = false;

    // Call this once at startup, passing IConfiguration
    public static void Initialize(IConfiguration configuration)
    {
        _securityKey = configuration["jwt:Key"];
        _expiryInMinutes = int.Parse(configuration["jwt:ExpiryInMinutes"]);
        _issuer = configuration["jwt:Issuer"];
        _audience = configuration["jwt:Audience"];
        _isInitialized = true;
    }

    public static string GenerateToken(string username)
    {
        if (!_isInitialized)
            throw new InvalidOperationException("JwtTokenGenerator not initialized. Call Initialize() first.");

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.UniqueName, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securityKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_expiryInMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
