using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

static public class UserController
{

    public static void registerUserRoutes(WebApplication app, DbContext lib)
    {
        // creating user
        app.MapPost("/user/signup", (SignUpRequest user) =>
        {
            lib.Database.ExecuteSqlRaw("check if user exist first if not then sign up");
        });

        // checking if user exist
        app.MapPost("/user/login", (LoginRequest lr) =>
        {
            lib.Database.ExecuteSqlRaw("login check");
        });

        //get all books issued by user
        app.MapGet("/user/getbooks/{username:string}", (string username) =>
        {
            lib.Database.ExecuteSqlRaw("Get books ");
        });

        //update user details
        app.MapPut("/user/update/{username:string}", (SignUpRequest user,string username) =>
        {
            lib.Database.ExecuteSqlRaw("update user");
        });

        //return a book
        app.MapGet("/user/bookreturn/{username:string}/{bookid:int}", (string username, int bookid) =>
        {
            lib.Database.ExecuteSqlRaw("add record increase book count remove from list");
        });
    }    
}