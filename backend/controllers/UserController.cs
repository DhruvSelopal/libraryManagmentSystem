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
            int users = lib.Database.ExecuteSqlRaw("SELECT UserName FROM dbo.Users WHERE username={0}",user.UserName);
            if (users == -1)
            {
                lib.Database.ExecuteSqlRaw(@"
                INSERT INTO dbo.Users
                (UserName, FirstName, LastName, PhoneNo, UserAddress, password, email, age)
                VALUES
                ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7});
            ",
                user.UserName,
                user.FirstName,
                user.LastName,
                user.PhoneNo,
                user.UserAddress,
                user.password,
                user.email,
                user.age);
                Console.WriteLine("Values added");
                return Results.Ok("User added successfully");
            }
            else return Results.BadRequest($"User exists {users}");
        });

        Console.WriteLine("code is reaching till this point");

        // checking if user exist
        app.MapPost("/user/login", (LoginRequest lr) =>
        {
            return Results.Ok();
            // lib.Database.ExecuteSqlRaw("login check");
        });

        //get all books issued by user
        app.MapGet("/user/getbooks/{username}", (string username) =>
        {
            return Results.Ok();
            // lib.Database.ExecuteSqlRaw("Get books ");
        });

        //update user details
        app.MapPut("/user/update/{username}", (SignUpRequest user, string username) =>
        {
            return Results.Ok();
            // lib.Database.ExecuteSqlRaw("update user");
        });

        //return a book
        app.MapGet("/user/bookreturn/{username}/{bookid:int}", (string username, int bookid) =>
        {
            return Results.Ok();
            // lib.Database.ExecuteSqlRaw("add record increase book count remove from list");
        });
    }

}