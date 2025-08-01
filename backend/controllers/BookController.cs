using Microsoft.EntityFrameworkCore;

static public class BookController
{
    static Dictionary<string, string> bookQueries = new Dictionary<string, string>
    {
        { "getallbooks","SELECT * FROM books"}
    };
    public static void RegisterBookRoutes(WebApplication app, DbContext lib)
    {
        bookQueries = new Dictionary<string, string>();
        bookQueries.Add("getallbooks", "SELECT * FROM books");
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