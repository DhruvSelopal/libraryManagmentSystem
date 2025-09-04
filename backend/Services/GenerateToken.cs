using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace TokenGeneration
{

    public  static class JwtTokenGenerator
    {
        private static bool _isInitialized = false;
        private static RefreshClaimsData _refreshclaimsdata;
        private static AcessClaimsData _accessclaimsdata;

        // Call this once at startup, passing IConfiguration
        public  static void Initialize(IConfiguration configuration)
        {
            _refreshclaimsdata = new RefreshClaimsData(configuration);
            _accessclaimsdata = new AcessClaimsData(configuration);
            _isInitialized = true;
        }

        public static string GenerateAcessToken(string username)
        {
            if (!_isInitialized)
                throw new InvalidOperationException("JwtTokenGenerator not initialized. Call Initialize() first.");

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.UniqueName, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_accessclaimsdata.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _accessclaimsdata.Issuer,
                audience: _accessclaimsdata.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_accessclaimsdata.ExpiryInMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static String GenerateRefreshToken(string username)
        {

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.UniqueName,username)
        };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_refreshclaimsdata.Key));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _refreshclaimsdata.Issuer,
                audience: _refreshclaimsdata.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(_refreshclaimsdata.ExpiryInDays),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
    public class RefreshClaimsData
    {
        public string Issuer;
        public string Key;
        public string Audience;
        public int ExpiryInDays;

        public RefreshClaimsData(IConfiguration config)
        {
            Issuer = config["Refresh:Issuer"];
            Key = config["Refresh:Key"];
            Audience = config["Refresh:Audience"];
            ExpiryInDays = int.Parse(config["Refresh:ExpiryInDays"]);
        }
    }
    public class AcessClaimsData
    {
        public string Issuer;
        public string Key;
        public string Audience;
        public int ExpiryInMinutes;


        public  AcessClaimsData(IConfiguration config)
        {
            Issuer = config["jwt:Issuer"];
            Key = config["jwt:Key"];
            Audience = config["jwt:Audience"];
            ExpiryInMinutes = int.Parse(config["jwt:ExpiryInMinutes"]);
        }
    }
}