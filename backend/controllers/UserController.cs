using System.IdentityModel.Tokens.Jwt;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.VisualBasic;

static public class UserController
{

    public static void registerUserRoutes(WebApplication app, LibraryContext lib,AuthService authservice)
    {
        Console.WriteLine("code is reaching till this point");
        // creating user
        app.MapPost("/user/signup", (SignUpRequest user) =>
{
    return SqlFunctions.CreateUser(lib, user)
        ? Results.Ok("User added successfully")
        : Results.BadRequest("User exists");
});

        app.MapPost("/user/login", async (LoginRequest lr) =>
        {
            if (await SqlFunctions.Login(lib, lr))
            {
                return Results.Ok(new
                {
                    Refreshtoken = TokenGeneration.JwtTokenGenerator.GenerateRefreshToken(lr.Username),
                    AcessToken = TokenGeneration.JwtTokenGenerator.GenerateAcessToken(lr.Username)
                 });
            }

            return Results.Unauthorized();
        }).AllowAnonymous();



        app.MapGet("/user/getbooks/{username}", (string username) =>
        {
            var books = SqlFunctions.GetBooksIssuedByUser(lib, username);
            return Results.Ok(books);
        });

        app.MapPut("/user/update/{username}", (SignUpRequest user, string username) =>
        {
            return SqlFunctions.UpdateUserDetails(lib, user, username)
                ? Results.Ok("User updated")
                : Results.BadRequest("Update failed");
        });

        app.MapGet("/user/bookreturn/{username}/{bookid:int}", (string username, int bookid) =>
        {
            return SqlFunctions.ReturnBook(lib, username, bookid)
                ? Results.Ok("Book returned successfully")
                : Results.BadRequest("Return failed");
        });

        app.MapGet("/user/bookissue/{username}/{bookid:int}", async (string username, int bookid, HttpRequest request) =>
        {
            await Task.Delay(5000);
            string refreshToken;
            if (!request.Headers.TryGetValue("refreshtoken", out var reftoken)) return Results.BadRequest("refresh token was missing");
            refreshToken = reftoken.ToString();


            if (!authservice.AuthenticateRefreshToken(refreshToken)){
                Console.WriteLine("Refresh token is invalid");
                return Results.BadRequest("invalid token ");
            }

            return SqlFunctions.IssueBook(lib, bookid, username) ? Results.Ok("Book issued successfully")
            : Results.BadRequest("Book issuing failed");
        }).RequireAuthorization();
    }

}