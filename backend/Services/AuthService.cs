using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
public class AuthService
{
    private WebApplicationBuilder _builder;
    private IConfigurationRoot _config;
    public AuthService(WebApplicationBuilder builder, IConfigurationRoot config)
    {
        _builder = builder;
        _config = config;
    }

    public void UseAcessAuthentication()
    {
        _builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_config["jwt:Key"])), // Use config instead of builder.Configuration

                ValidateIssuer = true,
                ValidIssuer = _config["jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = _config["jwt:Audience"],
                ValidateLifetime = true,
            };

            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                    return Task.CompletedTask;
                },
                OnTokenValidated = context =>
                {
                    Console.WriteLine("Token successfully validated");
                    return Task.CompletedTask;
                }
            };
        });
    }
    public bool AuthenticateRefreshToken(string token)
    {
        Console.WriteLine(_config["Refresh:Key"] + "this is the token");
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        Byte[] key = Encoding.UTF8.GetBytes(_config["Refresh:Key"]);
        

        TokenValidationParameters validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),

            ValidateIssuer = true,
            ValidIssuer = _config["Refresh:Issuer"],
            ValidateAudience = true,
            ValidAudience = _config["Refresh:Audience"],

            ValidateLifetime = true
        };

        try
        {
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out _);
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }
}