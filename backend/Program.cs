using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

class Program
{
    public static void Main(string[] args)
    {
        Environment.SetEnvironmentVariable("IdentityModelEventSource.ShowPII", "true");
        
        IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        string? dbUrl = config.GetConnectionString("DefaultConnection");

        DbContextOptions<LibraryContext> options = new DbContextOptionsBuilder<LibraryContext>()
            .UseSqlServer(dbUrl).Options;
        LibraryContext lib = new LibraryContext(options);

        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        
        // Add configuration to services
        builder.Services.AddSingleton(config);
        JwtTokenGenerator.Initialize(config);

        // builder.Services.AddCors(options =>
        // {
        //     options.AddPolicy("AllowAll", policy =>
        //     {
        //         policy.AllowAnyOrigin()
        //             .AllowAnyMethod()
        //             .AllowAnyHeader();
        //     });
        // });
        CorsService corssservice = new CorsService(builder);
        corssservice.useCors();

        // Add JWT authentication
        // builder.Services.AddAuthentication(options =>
        // {
        //     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        // })
        // .AddJwtBearer(options =>
        // {
        //     options.TokenValidationParameters = new TokenValidationParameters
        //     {
        //         ValidateIssuerSigningKey = true,
        //         IssuerSigningKey = new SymmetricSecurityKey(
        //             Encoding.UTF8.GetBytes(config["jwt:Key"])), // Use config instead of builder.Configuration

        //         ValidateIssuer = true,
        //         ValidIssuer = config["jwt:Issuer"],
        //         ValidateAudience = true,
        //         ValidAudience = config["jwt:Audience"],
        //         ValidateLifetime = true,
        //     };

        //     options.Events = new JwtBearerEvents
        //     {
        //         OnAuthenticationFailed = context =>
        //         {
        //             Console.WriteLine($"Authentication failed: {context.Exception.Message}");
        //             return Task.CompletedTask;
        //         },
        //         OnTokenValidated = context =>
        //         {
        //             Console.WriteLine("Token successfully validated");
        //             return Task.CompletedTask;
        //         }
        //     };
        // });

        AuthService authservice = new AuthService(builder, config);
        authservice.UseRefreshAuthentication();

        // In Program.cs
        builder.Services.AddEndpointsApiExplorer(); // Required for Minimal APIs
        // builder.Services.AddSwaggerGen(c =>
        // {
        //     c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
        //     {
        //         Title = "My API",
        //         Version = "v1",
        //         Description = "A sample API for demonstration purposes."
        //     });

        //     // Optional: Include XML comments for API documentation
        //     // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        //     // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        //     // c.IncludeXmlComments(xmlPath);
        // });
        
        builder.Services.AddAuthorization();
        
        WebApplication app = builder.Build();
        app.UseCors("AllowAll");
        



        // Add authentication middleware FIRST
        app.UseAuthentication();
        app.UseAuthorization();

        // Register your routes AFTER authentication middleware
        BookController.RegisterBookRoutes(app, lib);
        UserController.registerUserRoutes(app, lib);
       

        app.Run();
    }
}