using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

static public class UserController
{

    public static void registerUserRoutes(WebApplication app, LibraryContext lib)
    {
        Console.WriteLine("code is reaching till this point");
        // creating user
            app.MapPost("/user/signup", (SignUpRequest user) =>
    {
        return SqlFunctions.CreateUser(lib, user)
            ? Results.Ok("User added successfully")
            : Results.BadRequest("User exists");
    });

    app.MapPost("/user/login", (LoginRequest lr) =>
    {
        return SqlFunctions.Login(lib, lr)
            ? Results.Ok("Login successful")
            : Results.Unauthorized();
    });

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
    }

}