using Microsoft.EntityFrameworkCore;

static public class BookController
{
    public static void RegisterBookRoutes(WebApplication app, DbContext lib)
    {
        //get all books
        app.MapGet("/books", () =>
        {
            return Results.Ok();
            // return all books
        });

        // increase book count
        app.MapGet("/books/return/{bookid:int}", (int bookid) =>
        {
            return Results.Ok();
            //increment book count
        });

        //reduce book count
        app.MapGet("/books/issue/{bookid:int}", (int bookid) =>
        {
            return Results.Ok();
            //increase book count
        });
    }
}