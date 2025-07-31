using Microsoft.EntityFrameworkCore;

static public class BookController
{
    public static void RegisterBookRoutes(WebApplication app, DbContext lib)
    {
        //get all books
        app.MapGet("/books", () =>
        {
            // return all books
        });

        // increase book count
        app.MapGet("/book/return/{bookid:int}", (int bookid) =>
        {
            //increment book count
        });

        //reduce book count
        app.MapGet("/book/issue/{bookid:int}", (int bookid) =>
        {
            //increase book count
        });
    }
}