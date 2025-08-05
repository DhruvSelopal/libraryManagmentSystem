using Microsoft.EntityFrameworkCore;

static public class BookController
{
    public static void RegisterBookRoutes(WebApplication app, LibraryContext lib)
    {
        //get all books
        app.MapGet("/books/return/{bookid:int}", (int bookid) =>
    {
        return SqlFunctions.ReturnBookByBookId(lib, bookid)
            ? Results.Ok("Book count increased")
            : Results.BadRequest("Failed to update book count");
    });

    app.MapGet("/books/issue/{bookid:int}", (int bookid) =>
    {
        return SqlFunctions.IssueBookByBookId(lib, bookid)
            ? Results.Ok("Book count decreased")
            : Results.BadRequest("Failed to update book count");
    });
    }
}